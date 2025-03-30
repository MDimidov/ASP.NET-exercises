using System.ComponentModel.DataAnnotations;
using ErrorMessage = DeskMarket.Common.ErrorMessages;
using ProductConst = DeskMarket.Common.DataConstants.Product;

namespace DeskMarket.Models.Product
{
    public class ProductAddFormModel
    {
        [Required(ErrorMessage = ErrorMessage.Required)]
        [StringLength(ProductConst.ProductNameMaxLength, MinimumLength = ProductConst.ProductNameMinLength, ErrorMessage = ErrorMessage.StringLength)]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessage.Required)]
        [Range(typeof(decimal), ProductConst.PriceMinRange, ProductConst.PriceMaxRange, ErrorMessage = ErrorMessage.RangeError)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ErrorMessage.Required)]
        [StringLength(ProductConst.DescriptionMaxLength, MinimumLength = ProductConst.DescriptionMinLength, ErrorMessage = ErrorMessage.StringLength)]
        public string Description { get; set; } = string.Empty;

        [Url(ErrorMessage = ErrorMessage.UrlError)]
        public string? ImageUrl { get; set; }

        [Display(Name = "Added On")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        public string AddedOn = DateTime.UtcNow.ToString(ProductConst.AddedOnFormat);

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<ProductCategoryModel> Categories { get; set; } = new List<ProductCategoryModel>();

    }
}
