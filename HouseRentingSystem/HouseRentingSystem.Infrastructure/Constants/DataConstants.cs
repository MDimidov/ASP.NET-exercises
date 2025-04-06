namespace HouseRentingSystem.Infrastructure.Constants
{
    public static class DataConstants
    {
        public static class Category
        {
            // Name
            public const int NameMaxLength = 50;
        }

        public static class Agent
        {
            // Phone number
            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }

        public static class House
        {
            // Title
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;

            // Address
            public const int AddressMinLength = 30;
            public const int AddressMaxLength = 150;

            // Description
            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;

            // PricePerMonth
            public const string PricePerMonthMinRange = "0";
            public const string PricePerMonthMaxRange = "2000";

            // Image URL
            public const int ImageUrlMaxLength = 2048;
        }
    }
}
