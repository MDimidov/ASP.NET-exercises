using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using CategoryConst = HouseRentingSystem.Infrastructure.Constants.DataConstants.Category;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    [Comment("House category")]
    public class Category
    {
        [Key]
        [Comment("Category identifier")]
        public int Id { get; init; }

        [Required]
        [StringLength(CategoryConst.NameMaxLength)]
        [Comment("Category name")]
        public required string Name { get; set; }

        public virtual ICollection<House> Houses { get; set; } = new List<House>();
    }
}
