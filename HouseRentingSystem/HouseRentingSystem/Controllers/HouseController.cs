using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;

        public HouseController(
            IHouseService houseService,
            IAgentService agentService
            )
        {
            this.houseService = houseService;
            this.agentService = agentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(new AllHousesQueryModel());
        }

        [HttpGet]
        public IActionResult Mine()
        {
            return View(new AllHousesQueryModel());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(new HouseDetailsViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (!await agentService.IsExistByIdAsync(User.Id()))
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            HouseFormModel model = new()
            {
                Categories = await houseService.GetAllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            if (!await agentService.IsExistByIdAsync(User.Id()))
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (!await houseService.IsCategoryExistByIdAsync(model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Please choose valid category");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await houseService.GetAllCategoriesAsync();
                return View(model);
            }


            int houseId = await houseService.AddHouseAsync(model, await agentService.GetAgentIdByUserId(User.Id()));

            return RedirectToAction(nameof(Details), new { id = houseId });
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View(new HouseFormModel());
        }

        [HttpPost]
        public IActionResult Edit(HouseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new HouseDeleteViewModel());
        }

        [HttpPost]
        public IActionResult Delete(HouseDeleteViewModel model)
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Rent()
        {
            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public IActionResult Leave()
        {
            return RedirectToAction(nameof(Mine));
        }
    }
}
