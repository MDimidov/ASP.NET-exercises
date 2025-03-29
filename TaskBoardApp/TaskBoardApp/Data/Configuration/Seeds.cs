using Microsoft.AspNetCore.Identity;
using TaskBoardApp.Data.Models;

namespace TaskBoardApp.Data.Configuration
{
    public static class Seeds
    {
        public static Board OpenBoard = new()
        {
            Id = 1,
            Name = "Open"
        };

        public static Board InProgressBoard = new()
        {
            Id = 2,
            Name = "In Progress"
        };

        public static Board DoneBoard = new()
        {
            Id = 3,
            Name = "Done"
        };

        public static IdentityUser TestUser = SeedUsers();

        private static IdentityUser SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var testUser = new IdentityUser()
            {
                UserName = "test@softuni.bg",
                NormalizedUserName = "TEST@SOFTUNI.BG"
            };

            testUser.PasswordHash = hasher.HashPassword(testUser, "softuni");

            return testUser;
        }

        
    }
}
