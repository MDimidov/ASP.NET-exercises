using Practice.Models.Product;

namespace Practice.Contracts
{
    public interface IProductService
    {
        ProductViewModel? GetProductById(int id, IEnumerable<ProductViewModel> products);
    }
}
