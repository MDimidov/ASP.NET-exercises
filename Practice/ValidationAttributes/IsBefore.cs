using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Practice.ValidationAttributes
{
    public class IsBefore : ValidationAttribute
    {
        private const string DateFormat = "dd/MM/yyyy";
        private readonly DateTime date;

        public IsBefore(string dateInput)
        {
            date = DateTime.ParseExact(dateInput, DateFormat, CultureInfo.InvariantCulture);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && (DateTime)value >= date)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
