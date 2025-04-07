using System.ComponentModel.DataAnnotations;
using HouseConst = HouseRentingSystem.Infrastructure.Constants.DataConstants.House;
using ErrorMessage = HouseRentingSystem.Infrastructure.Constants.ErrorMessages;

namespace HouseRentingSystem.Core.Models.House
{
    public class HouseFormModel
    {
        [Required(ErrorMessage = ErrorMessage.Required)]
        [StringLength(HouseConst.TitleMaxLength, MinimumLength = HouseConst.TitleMinLength, ErrorMessage = ErrorMessage.StringLength)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessage.Required)]
        [StringLength(HouseConst.AddressMaxLength, MinimumLength = HouseConst.AddressMinLength, ErrorMessage = ErrorMessage.StringLength)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessage.Required)]
        [StringLength(HouseConst.DescriptionMaxLength, MinimumLength = HouseConst.DescriptionMinLength, ErrorMessage = ErrorMessage.StringLength)]
        public string Description { get; set; } = string.Empty;

        [Url]
        [Display(Name = "Image URL")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [StringLength(HouseConst.ImageUrlMaxLength, ErrorMessage = ErrorMessage.StringLength)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessage.Required)]
        [Display(Name = "Price Per Month")]
        [Range(typeof(decimal), HouseConst.PricePerMonthMinRange, HouseConst.PricePerMonthMaxRange, ErrorMessage = ErrorMessage.Range)]
        public decimal PricePerMonth { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } = new HashSet<HouseCategoryServiceModel>();
    }
}
