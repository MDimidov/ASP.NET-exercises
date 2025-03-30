using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskMarket.Data.Models
{
    [Comment("Mapping table for Product and IdentityUser")]
    public class ProductClient
    {
        [Required]
        [Comment("Product identifier")]
        public required int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Product Product { get; set; } = null!;

        [Required]
        [Comment("Application user identifier")]
        public required string ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual IdentityUser Client { get; set; } = null!;
    }
}

//•	Has ProductId – integer, PrimaryKey, foreign key (required)
//•	Has Product – Product
//•	Has ClientId – string, PrimaryKey, foreign key (required)
//•	Has Client – IdentityUser
