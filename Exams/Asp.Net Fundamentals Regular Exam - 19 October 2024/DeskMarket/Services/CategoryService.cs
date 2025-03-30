using DeskMarket.Contracts;
using DeskMarket.Data;
using DeskMarket.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductCategoryModel>> GetAllForFormAsync()
            => await context.Categories
            .Select(c => new ProductCategoryModel
            {
                Name = c.Name,
                Id = c.Id
            })
            .ToListAsync();
    }
}
