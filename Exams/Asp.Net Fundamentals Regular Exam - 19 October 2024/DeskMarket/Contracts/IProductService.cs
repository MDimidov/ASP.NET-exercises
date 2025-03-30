using DeskMarket.Models.Product;

namespace DeskMarket.Contracts
{
    public interface IProductService
    {
        Task AddProductAsync(ProductAddFormModel model, string userId);
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string userId);
    }
}
