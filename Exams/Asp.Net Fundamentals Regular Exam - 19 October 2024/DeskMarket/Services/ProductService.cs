using DeskMarket.Contracts;
using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models.Product;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static DeskMarket.Common.DataConstants.Product;

namespace DeskMarket.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddProductAsync(ProductAddFormModel model, string userId)
        {
            DateTime.TryParseExact(model.AddedOn, AddedOnFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime addedOn);

            Product productToAdd = new()
            {
                ProductName = model.ProductName,
                Price = model.Price,
                SellerId = userId,
                AddedOn = addedOn,
                CategoryId = model.CategoryId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };

            await context.Products.AddAsync(productToAdd);
            await context.SaveChangesAsync();
        }

        public async Task AddProductToCardAsync(int id, string userId)
        {
            ProductClient? productClientCheck = await context.ProductClients.FirstOrDefaultAsync(pc => pc.ProductId == id && pc.ClientId == userId);

            if (productClientCheck == null)
            {
                ProductClient productClient = new()
                {
                    ProductId = id,
                    ClientId = userId,
                };

                await context.ProductClients.AddAsync(productClient);
                await context.SaveChangesAsync();
            }
        }

        public async Task EditProductAsync(ProductEditFormModel model, int productId)
        {
            Product? productEntity = await context.Products.FindAsync(productId);

            if (productEntity == null)
            {
                throw new ArgumentException($"No such product with id: {productId}");
            }

            DateTime.TryParseExact(model.AddedOn, AddedOnFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime addedOn);


            productEntity.ProductName = model.ProductName;
            productEntity.Price = model.Price;
            productEntity.Description = model.Description;
            productEntity.ImageUrl = model.ImageUrl;
            productEntity.CategoryId = model.CategoryId;
            productEntity.AddedOn = addedOn;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string userId)
            => await context.Products
                .Where(p => !p.IsDeleted)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    ProductName = p.ProductName,
                    IsSeller = p.SellerId == userId,
                    HasBought = p.ProductsClients.Any(pc => pc.ClientId == userId),
                })
                .ToListAsync();

        public async Task<IEnumerable<ProductCartViewModel>> GetCartProductByUserIdAsync(string userId)
            => await context.Products
            .Where(p => p.SellerId != userId && !p.IsDeleted &&
                p.ProductsClients.Any(pc => pc.ClientId == userId))
            .Select(p => new ProductCartViewModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
            })
            .ToListAsync();

        public async Task<ProductEditFormModel?> GetProductByIdAsync(int id)
            => await context.Products
            .Where(p => !p.IsDeleted && p.Id == id)
            .Select(p => new ProductEditFormModel
            {
                SellerId = p.SellerId,
                CategoryId = p.CategoryId,
                ProductName = p.ProductName,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                AddedOn = p.AddedOn.ToString(AddedOnFormat),
            })
            .FirstOrDefaultAsync();

        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int id)
        {
            var model = await context.Products
                .Where(p => p.Id == id && !p.IsDeleted)
                .Select(p => new ProductDetailsViewModel
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    AddedOn = p.AddedOn.ToString(AddedOnFormat),
                    CategoryName = p.Category.Name,
                    Seller = p.Seller.UserName!,
                    HasBought = p.ProductsClients.Any(pc => pc.ProductId == id),
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new ArgumentException($"No such a product with id: {id}");
            }

            return model;
        }

        public async Task RemoveProductFromCardAsync(int id, string userId)
        {
            ProductClient? productClient = await context.ProductClients.FirstOrDefaultAsync(pc => pc.ProductId == id && pc.ClientId == userId);

            if (productClient == null)
            {
                throw new ArgumentException($"No such a cart product with id: {id}");
            }

            context.ProductClients.Remove(productClient);
            await context.SaveChangesAsync();
        }
    }
}
