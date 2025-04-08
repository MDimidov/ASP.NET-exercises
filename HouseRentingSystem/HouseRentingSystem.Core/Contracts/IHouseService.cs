﻿using HouseRentingSystem.Core.Enums;
using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Contracts
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();

        Task<IEnumerable<HouseCategoryServiceModel>> GetAllCategoriesAsync();
        Task<int> AddHouseAsync(HouseFormModel houseModel, int agentId);
        Task<bool> IsCategoryExistByIdAsync(int categoryId);
        Task<IEnumerable<string>> GetCategoriesNamesAsync();
        Task<HouseQueryServiceModel> AllQueryableAsync(
            string? category = null,
            string? searchTerm = null,
            HouseSorting sorting = HouseSorting.Newest,
            int currentPage = 1,
            int housesPerPage = 1);
        Task<HouseDetailsViewModel?> GetHouseDetailsByIdAsync(int houseId);
        Task<IEnumerable<HouseServiceModel>> GetMineHousesByAgentIdAsync(int agentId);
        Task<IEnumerable<HouseServiceModel>> GetMineHousesByUserIdAsync(string userId);
    }
}
