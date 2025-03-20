using Practice.Contracts;
using Practice.Models.Product;

namespace Practice.Services
{
    public class ProductService : IProductService
    {
        public ProductViewModel? GetProductById(int id, IEnumerable<ProductViewModel> products)
         => products.FirstOrDefault(p => p.Id == id);
    }
}
