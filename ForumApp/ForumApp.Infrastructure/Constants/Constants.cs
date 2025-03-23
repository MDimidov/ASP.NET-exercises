namespace ForumApp.Infrastructure.Constants
{
    public static class Constants
    {
        public static class PostConstants
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;
            public const int ContentMinLength = 30;
            public const int ContentMaxLength = 1500;
        }

        public class ErrorMessages
        {
            public const string RequredMessage = "Field {0} is requred";
            public const string LengthMessage = "Field {0} must be between {2} and {1} characters long";
        }
    }
}
