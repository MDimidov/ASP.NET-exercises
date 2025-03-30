using DeskMarket.Models.Product;

namespace DeskMarket.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string userId);
    }
}
