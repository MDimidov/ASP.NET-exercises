using System.ComponentModel.DataAnnotations;

namespace Practice.ValidationAttributes
{
    public class IsAdult : ValidationAttribute
    {
        private readonly DateTime minimumAge = DateTime.UtcNow.AddYears(-18);
        private readonly int adultAge = 18;

        public IsAdult()
        {
        }

        public IsAdult(int adultAge)
        {
            this.adultAge = adultAge;
            minimumAge = DateTime.UtcNow.AddYears(-1 * adultAge);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && (DateTime)value >= minimumAge)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
