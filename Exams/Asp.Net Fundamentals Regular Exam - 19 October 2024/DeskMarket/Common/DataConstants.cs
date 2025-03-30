namespace DeskMarket.Common
{
    public static class DataConstants
    {
        public static class Product
        {
            // ProductName
            public const int ProductNameMinLength = 2;
            public const int ProductNameMaxLength = 60;

            // Description
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 250;

            // Price
            public const string PriceMinRange = "1.0";
            public const string PriceMaxRange = "3_000.0";

            // AddedOn Format
            public const string AddedOnFormat = "dd-MM-yyyy";
        }

        public static class Category
        {
            // Name
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;
        }
    }
}
