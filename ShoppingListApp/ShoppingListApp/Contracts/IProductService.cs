using ShoppingListApp.Models;

namespace ShoppingListApp.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();

        Task<ProductViewModel> GetProductByIdAsync(int id);

        Task UpdateProductAsync(ProductViewModel productViewModel);

        Task DeleteProductByIdAsync(int id);

        Task CreateProductAsync(ProductViewModel productViewModel);
    }
}
