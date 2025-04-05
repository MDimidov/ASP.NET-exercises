using Practice.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models.Home
{
    public class HomeViewModel : IValidatableObject
    {
        [Required]
        [IsBefore("05/04/2025", ErrorMessage = "Date must be before 05/04/2025")]
        public DateTime CheckDate { get; set; }

        [Required]
        [IsAdult(2, ErrorMessage = "You must be at least 2 years old")]
        public DateTime BirthDate { get; set; }

        public string? Name { get; set; }

        public string? Country { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult("Name is required", new[] { nameof(Name) });
            }

            if (string.IsNullOrWhiteSpace(Country))
            {
                yield return new ValidationResult("Country is required", new[] { nameof(Country) });
            }

            if (Name == "Pesho" && (Country?.ToUpper() != "BG" && Country?.ToUpper() != "BULGARIA"))
            {
                yield return new ValidationResult(
                    $"If name is {Name} county must be BG or Bulgaria", 
                    new[] { nameof(Name), nameof(Country) });
            }
        }
    }
}
