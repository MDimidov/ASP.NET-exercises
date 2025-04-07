using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
    public class HouseService : IHouseService
    {
        private readonly HouseRentingDbContext context;

        public HouseService(HouseRentingDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<HouseCategoryServiceModel>> GetAllCategoriesAsync()
            => await context.Categories
                .AsNoTracking()
                .Select(c => new HouseCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

        public async Task<bool> IsCategoryExistByIdAsync(int categoryId)
            => await context.Categories
                .AnyAsync(c => c.Id == categoryId);

        public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync()
            => await context.Houses
                .AsNoTracking()
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseIndexServiceModel()
                {
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    Title = h.Title,
                })
                .Take(3)
                .ToArrayAsync();

        public async Task<int> AddHouseAsync(HouseFormModel houseModel, int agentId)
        {
            try
            {
                House house = new()
                {
                    Title = houseModel.Title,
                    Address = houseModel.Address,
                    Description = houseModel.Description,
                    ImageUrl = houseModel.ImageUrl,
                    PricePerMonth = houseModel.PricePerMonth,
                    CategoryId = houseModel.CategoryId,
                    AgentId = agentId
                };

                await context.Houses.AddAsync(house);
                await context.SaveChangesAsync();

                return house.Id;
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to add House");
            }
        }
    }
}
