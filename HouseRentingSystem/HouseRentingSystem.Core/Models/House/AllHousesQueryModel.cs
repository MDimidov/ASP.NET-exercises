using HouseRentingSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystem.Core.Models.House
{
    public class AllHousesQueryModel
    {
        public const int HousesPerPage = 3;

        public string Category { get; init; } = string.Empty;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; } = string.Empty;

        public HouseSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalHousesCount { get; set; }

        public IEnumerable<string> Categories = new HashSet<string>();

        public IEnumerable<HouseServiceModel> Houses = new HashSet<HouseServiceModel>();
    }
}
