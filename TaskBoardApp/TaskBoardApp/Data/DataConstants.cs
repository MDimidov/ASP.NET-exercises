namespace TaskBoardApp.Data
{
    public class DataConstants
    {
        public static class Task
        {
            // Title
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 70;

            // Description
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 1_000;
        }

        public static class Board
        {
            // Name
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
        }
    }
}
