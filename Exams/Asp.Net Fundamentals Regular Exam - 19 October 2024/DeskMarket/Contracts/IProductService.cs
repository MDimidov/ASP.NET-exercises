using DeskMarket.Models.Product;

namespace DeskMarket.Contracts
{
    public interface IProductService
    {
        Task AddProductAsync(ProductAddFormModel model, string userId);
        Task AddProductToCardAsync(int id, string userId);
        Task EditProductAsync(ProductEditFormModel model, int productId);
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync(string userId);
        Task<IEnumerable<ProductCartViewModel>> GetCartProductByUserIdAsync(string userId);
        Task<ProductEditFormModel?> GetProductByIdAsync(int id);
        Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(int id);
    }
}
