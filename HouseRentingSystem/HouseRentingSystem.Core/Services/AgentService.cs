using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Infrastructure;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
    public class AgentService : IAgentService
    {

        private readonly HouseRentingDbContext context;
        private readonly IRepository repository;

        public AgentService(
            HouseRentingDbContext context, 
            IRepository repository)
        {
            this.context = context;
            this.repository = repository;
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

        public async Task<int> GetAgentIdByUserIdAsync(string userId)
        {
            try
            {
                int agentId = await repository.AllAsReadOnly<Agent>()
                    .Where(a => a.UserId == userId)
                    .Select(a => a.Id)
                    .FirstOrDefaultAsync();

                return agentId;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> IsExistByIdAsync(string userId)
            => await repository.AllAsReadOnly<Agent>().AnyAsync(a => a.UserId == userId);

        public async Task<bool> IsExistByPhoneNumberAsync(string phoneNumber)
            => await repository.AllAsReadOnly<Agent>().AnyAsync(a => a.PhoneNumber == phoneNumber);

        public async Task<bool> IsUserHasRentsAsync(string userId)
            => await repository.AllAsReadOnly<House>().AnyAsync(h => h.RenterId == userId);
    }
}
