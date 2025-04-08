using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Enums;
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

        public async Task<IEnumerable<string>> GetCategoriesNamesAsync()
            => await context.Categories
                .AsNoTracking()
                .Select(c => c.Name)
                .Distinct()
                .ToArrayAsync();

        public async Task<HouseQueryServiceModel> AllQueryableAsync(
            string? category, 
            string? searchTerm, 
            HouseSorting sorting, 
            int currentPage, 
            int housesPerPage)
        {
            IQueryable<House> housesQuery = context
                .Houses
                .Include(h => h.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                housesQuery = housesQuery
                    .Where(h => h.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                housesQuery = housesQuery
                    .Where(h =>
                        h.Title.ToLower().Contains(searchTerm.ToLower()) ||
                        h.Address.ToLower().Contains(searchTerm.ToLower()) ||
                        h.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            housesQuery = sorting switch
            {
                HouseSorting.Oldest => housesQuery
                    .OrderBy(h => h.Id),
                HouseSorting.PriceAscending => housesQuery
                    .OrderBy (h => h.PricePerMonth),
                HouseSorting.PriceDescending => housesQuery
                    .OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery
                    .OrderBy(h => h.RenterId != null)
                    .ThenByDescending(h => h.Id),
                _ => housesQuery.OrderByDescending(h => h.Id),
            };

            var houses  = await housesQuery
                .AsNoTracking()
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                })
                .ToListAsync();

            int totalHousesCount = housesQuery.Count();

            return new HouseQueryServiceModel()
            {
                TotalHousesCount = totalHousesCount,
                Houses = houses,
            };
        }
    }
}
