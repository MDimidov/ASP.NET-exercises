namespace DeskMarket.Models.Product
{
    public class ProductDetailsViewModel : ProductCartViewModel
    {
        public required string Description { get; set; }

        public required string CategoryName { get; set; }

        public required bool HasBought { get; set; }

        public required string Seller { get; set; }

        public required string AddedOn { get; set; }
    }
}
