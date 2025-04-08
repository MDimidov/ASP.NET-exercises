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
                    .OrderBy(h => h.PricePerMonth),
                HouseSorting.PriceDescending => housesQuery
                    .OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery
                    .OrderBy(h => h.RenterId != null)
                    .ThenByDescending(h => h.Id),
                _ => housesQuery.OrderByDescending(h => h.Id),
            };

            var houses = await housesQuery
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

        public async Task<HouseDetailsViewModel?> GetHouseDetailsByIdAsync(int houseId)
            => await context
                .Houses
                .AsNoTracking()
                .Where(h => h.Id == houseId)
                .Select(h => new HouseDetailsViewModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    Description = h.Description,
                    Category = h.Category.Name,
                    IsRented = h.RenterId != null,
                    Agent = new Models.Agent.AgentServiceModel
                    {
                        PhoneNumber = h.Agent.PhoneNumber,
                        Email = h.Agent.User.Email ?? string.Empty,
                    }
                })
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<HouseServiceModel>> GetMineHousesByAgentIdAsync(int agentId)
            => await context.Houses
                .AsNoTracking()
                .Where(h => h.AgentId == agentId)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                })
                .ToArrayAsync();

        public async Task<IEnumerable<HouseServiceModel>> GetMineHousesByUserIdAsync(string userId)
            => await context.Houses
                .AsNoTracking()
                .Where(h => h.RenterId == userId)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                })
                .ToArrayAsync();

        public async Task<bool> EditHouseAsync(HouseFormModel model, int houseId)
        {
            House? houseEntity = await context.Houses.FindAsync(houseId);

            if (houseEntity == null)
            {
                return false;
            }

            houseEntity.Title = model.Title;
            houseEntity.Address = model.Address;
            houseEntity.Description = model.Description;
            houseEntity.ImageUrl = model.ImageUrl;
            houseEntity.PricePerMonth = model.PricePerMonth;
            houseEntity.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsHouseExistById(int houseId)
            => await context.Houses.AnyAsync(h => h.Id == houseId);

        public async Task<bool> IsUserOwnerByIdAsync(string userId, int houseId)
            => await context.Houses.AnyAsync(h => h.Agent.UserId == userId && h.Id == houseId);

        public async Task<HouseFormModel> GetHouseForEditAsync(int houseId)
            => await context.Houses
            .Where(h => h.Id == houseId)
            .Select(h => new HouseFormModel
            {
                Title = h.Title,
                Address = h.Address,
                Description = h.Description,
                ImageUrl = h.ImageUrl,
                PricePerMonth = h.PricePerMonth,
                CategoryId = h.CategoryId,
            })
            .FirstAsync();

        public async Task<bool> DeleteHouseByIdAsync(int houseId)
        {
            House? houseEntity = await context.Houses.FindAsync(houseId);

            if(houseEntity == null)
            {
                return false;
            }

            context.Houses.Remove(houseEntity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RentHouseByIdAsync(string userId, int houseId)
        {
            House? houseEntity = await context.Houses.FindAsync(houseId);

            if (houseEntity == null)
            {
                return false;
            }

            houseEntity.RenterId = userId;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsRentedByIdAsync(int houseId)
            => await context.Houses.AnyAsync(h => h.RenterId != null && h.Id == houseId);

        public async Task<bool> IsUserRentHouseByIdAsync(string userId, int houseId)
            => await context.Houses.AnyAsync(h => h.RenterId == userId && h.Id == houseId);

        public async Task<bool> LeaveHouseAsync(int houseId)
        {
            House? houseEntity = await context.Houses.FindAsync(houseId);

            if (houseEntity == null)
            {
                return false;
            }

            houseEntity.RenterId = null;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
