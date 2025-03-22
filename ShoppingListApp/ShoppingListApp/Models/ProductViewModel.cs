using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Models
{
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters!")]
        [DisplayName("Product Name")]
        public required string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
