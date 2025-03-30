using DeskMarket.Models.Product;

namespace DeskMarket.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<ProductCategoryModel>> GetAllForFormAsync();
    }
}
