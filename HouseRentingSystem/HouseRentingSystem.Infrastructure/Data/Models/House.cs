using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HouseConst = HouseRentingSystem.Infrastructure.Constants.DataConstants.House;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    [Comment("House for rent")]
    public class House
    {
        [Key]
        [Comment("House identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(HouseConst.TitleMaxLength)]
        [Comment("House title")]
        public required string Title { get; set; }

        [Required]
        [StringLength(HouseConst.AddressMaxLength)]
        [Comment("House address")]
        public required string Address { get; set; }

        [Required]
        [StringLength(HouseConst.DescriptionMaxLength)]
        [Comment("House description")]
        public required string Description { get; set; }

        [Required]
        [StringLength(HouseConst.ImageUrlMaxLength)]
        [Comment("House image URL")]
        public required string ImageUrl { get; set; }

        [Required]
        [Comment("House price per month")]
        //[Range(typeof(decimal), HouseConst.PricePerMonthMinRange, HouseConst.PricePerMonthMaxRange)]
        [Precision(18, 2)]
        public required decimal PricePerMonth {  get; set; }

        [Required]
        [Comment("House category identifier")]
        public required int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Category Category { get; set; } = null!;

        [Required]
        [Comment("House agent identifier")]
        public required int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Agent Agent { get; set; } = null!;

        [Comment("House renter identifier - Identity user")]
        public string? RenterId { get; set; }

        [ForeignKey(nameof(RenterId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual IdentityUser? Renter { get; set; }
    }
}