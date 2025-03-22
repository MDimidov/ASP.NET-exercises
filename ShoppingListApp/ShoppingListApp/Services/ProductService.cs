using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Contracts;
using ShoppingListApp.Data;
using ShoppingListApp.Data.Models;
using ShoppingListApp.Models;

namespace ShoppingListApp.Services
{
    public class ProductService : IProductService
    {
        private readonly ShoppingListDbContext context;

        public ProductService(ShoppingListDbContext context)
        {
            this.context = context;
        }

        public async Task CreateProductAsync(ProductViewModel productViewModel)
        {
            Product product = new()
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price,
            };

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProductByIdAsync(int id)
        {
            Product? product = await context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ArgumentException("No such a product");
            }

            context.Remove<Product>(product!);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        => await context.Products
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                })
                .ToListAsync();

        public async Task<ProductViewModel> GetProductByIdAsync(int id)
        {
            Product? product = await context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ArgumentException("No such a product");
            }

            return new ProductViewModel() { Id = product.Id, Name = product.Name, Price = product.Price };
        }

        public async Task UpdateProductAsync(ProductViewModel productViewModel)
        {
            Product? product = await context.Products.FindAsync(productViewModel.Id);

            if (product == null)
            {
                throw new ArgumentException("No such a product");
            }

            product.Name = productViewModel.Name;
            product.Price = productViewModel.Price;

            context.Update(product);
            await context.SaveChangesAsync();
        }
    }
}
