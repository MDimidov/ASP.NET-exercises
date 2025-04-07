using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HouseRentingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHouseService houseService;

        public HomeController(IHouseService houseService)
        {
            this.houseService = houseService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await houseService.LastThreeHousesAsync();
            return View(model);
        }
    }
}
