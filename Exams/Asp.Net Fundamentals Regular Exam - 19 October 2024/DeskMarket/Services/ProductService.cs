using DeskMarket.Contracts;
using DeskMarket.Data;
using DeskMarket.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
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
