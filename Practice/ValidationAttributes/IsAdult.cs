using System.ComponentModel.DataAnnotations;

namespace Practice.ValidationAttributes
{
    public class IsAdult : ValidationAttribute
    {
        private readonly DateTime minimumAge = DateTime.UtcNow.AddYears(-18);

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && (DateTime)value < minimumAge)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
