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

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string userId)
            => await context.Products
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
    }
}
