using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp.Data.Models
{
    [Comment("Product Note")]
    public class ProductNote
    {
        [Key]
        [Comment("Note Identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Comment("Note Content")]
        public required string Content { get; set; }

        [Required]
        [Comment("Product Identifier")]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey(nameof(ProductId))]
        public required Product Product { get; set; }
    }
}
