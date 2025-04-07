using System.Security.Claims;

namespace HouseRentingSystem.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    }
}
