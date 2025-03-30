using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using CategoryConstants = DeskMarket.Common.DataConstants.Category;

namespace DeskMarket.Data.Models
{
    [Comment($"{nameof(Category)} table")]
    public class Category
    {
        [Key]
        [Comment($"{nameof(Category)} identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryConstants.NameMaxLength)]
        [Comment($"{nameof(Category)} name")]
        public required string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

//•	Has Id – a unique integer, Primary Key
//•	Has Name – a string with min length 3 and max length 20 (required)
//•	Has Products – a collection of type Product
