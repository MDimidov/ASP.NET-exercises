using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductConstants = DeskMarket.Common.DataConstants.Product;

namespace DeskMarket.Data.Models
{

    [Comment($"{nameof(Product)} table")]
    public class Product
    {
        [Key]
        [Comment($"{nameof(Product)} identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(ProductConstants.ProductNameMaxLength)]
        [Comment($"{nameof(Product)} name")]
        public required string ProductName { get; set; }

        [Required]
        [StringLength(ProductConstants.DescriptionMaxLength)]
        [Comment($"{nameof(Product)} description")]
        public required string Description { get; set; }

        [Required]
        [Precision(18, 2)]
        [Range(typeof(decimal), ProductConstants.PriceMinRange, ProductConstants.PriceMaxRange)]
        [Comment($"{nameof(Product)} price")]
        public required decimal Price { get; set; }

        [Url]
        [Comment($"{nameof(Product)} image URL")]
        public string? ImageUrl { get; set; }

        [Required]
        [Comment($"{nameof(Product)} application user identifier")]
        public required string SellerId { get; set; }

        [Required]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        [ForeignKey(nameof(SellerId))]
        public virtual required IdentityUser Seller { get; set; }

        [Required]
        [Comment($"{nameof(Product)} date of creation")]
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;

        [Required]
        [Comment($"{nameof(Product)} category identifier")]
        public required int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual required Category Category { get; set; }

        [Required]
        [Comment($"{nameof(Product)} is deleted")]
        public bool IsDeleted { get; set; } = false;

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual ICollection<ProductClient> ProductsClients { get; set; } = new HashSet<ProductClient>();
    }
}


//•	Has AddedOn – DateTime with format "dd-MM-yyyy" (required)
//•	Has CategoryId – integer, foreign key (required)
//•	Has Category – Category (required)
//•	Has IsDeleted – bool (default value == false)
//•	Has ProductsClients – a collection of type ProductClient
