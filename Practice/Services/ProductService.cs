using Practice.Contracts;
using Practice.Models.Product;
using System.Text;

namespace Practice.Services
{
    public class ProductService : IProductService
    {
        public string GetAllProductsAsPlainText(IEnumerable<ProductViewModel> products)
        {
            StringBuilder stringBuilder = new ();

            foreach (ProductViewModel product in products)
            {
                stringBuilder.AppendLine($"Product {product.Id}: {product.Name} - {product.Price} lv.");
            }

            return stringBuilder.ToString();
        }

        public ProductViewModel? GetProductById(int id, IEnumerable<ProductViewModel> products)
         => products.FirstOrDefault(p => p.Id == id);
    }
}
