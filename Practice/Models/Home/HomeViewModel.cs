using Practice.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models.Home
{
    public class HomeViewModel
    {
        [Required]
        [IsBefore("05/04/2025", ErrorMessage = "Date must be before 05/04/2025")]
        public DateTime BirthDate { get; set; }
    }
}
