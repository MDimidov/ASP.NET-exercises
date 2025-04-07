using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
    public class AgentService : IAgentService
    {

        private readonly HouseRentingDbContext context;

        public AgentService(HouseRentingDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateAsync(string userId, string phoneNumber)
        {
            try
            {
                Agent agent = new()
                {
                    PhoneNumber = phoneNumber,
                    UserId = userId
                };

                await context.Agents.AddAsync(agent);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)

            {
                return false;
            }
        }

        public async Task<bool> IsExistByIdAsync(string userId)
            => await context.Agents.AnyAsync(a => a.UserId == userId);

        public async Task<bool> IsExistByPhoneNumberAsync(string phoneNumber)
            => await context.Agents.AnyAsync(a => a.PhoneNumber == phoneNumber);

        public async Task<bool> IsUserHasRentsAsync(string userId)
            => await context.Houses.AnyAsync(h => h.RenterId == userId);
    }
}
