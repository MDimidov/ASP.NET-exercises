namespace HouseRentingSystem.Core.Contracts
{
    public interface IAgentService
    {
        Task<bool> IsExistByIdAsync(string userId);
        Task<bool> IsExistByPhoneNumberAsync(string phoneNumber);
        Task<bool> IsUserHasRentsAsync(string userId);
        Task<bool> CreateAsync(string userId, string phoneNumber);
    }
}
