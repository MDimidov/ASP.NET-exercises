using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();

        Task<IEnumerable<HouseCategoryServiceModel>> GetAllCategoriesAsync();
        Task<int> AddHouseAsync(HouseFormModel houseModel, int agentId);
        Task<bool> IsCategoryExistByIdAsync(int categoryId);
    }
}
