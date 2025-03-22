using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Data.Models
{
    [Comment("Shopping List Product table")]
    public class Product
    {
        [Key]
        [Comment("Product Identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Product Name")]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [Comment("Product Price")]
        public decimal Price { get; set; }

        public List<ProductNote> ProductNotes { get; set; } = new List<ProductNote>();
    }
}
