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

        [HttpGet, HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] AllHousesQueryModel query)
        {
            HouseQueryServiceModel queryResult = await houseService.AllQueryableAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            query.Categories = await houseService.GetCategoriesNamesAsync();

            return View(query);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            IEnumerable<HouseServiceModel> model;
            string userId = User.Id();
            if (await agentService.IsExistByIdAsync(userId))
            {
                int agentId = await agentService.GetAgentIdByUserIdAsync(userId);
                model = await houseService.GetMineHousesByAgentIdAsync(agentId);
            }
            else
            {
                model = await houseService.GetMineHousesByUserIdAsync(userId);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            HouseDetailsViewModel? model = await houseService.GetHouseDetailsByIdAsync(id);

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
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


            int houseId = await houseService.AddHouseAsync(model, await agentService.GetAgentIdByUserIdAsync(User.Id()));

            return RedirectToAction(nameof(Details), new { id = houseId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await houseService.IsHouseExistById(id))
            {
                return BadRequest();
            }

            string userId = User.Id();
            if (!await agentService.IsExistByIdAsync(userId))
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (!await houseService.IsUserOwnerByIdAsync(userId, id))
            {
                return Unauthorized();
            }

            HouseFormModel model = await houseService.GetHouseForEditAsync(id);
            model.Categories = await houseService.GetAllCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HouseFormModel model, int id)
        {
            string userId = User.Id();
            if (!await agentService.IsExistByIdAsync(userId))
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (!await houseService.IsHouseExistById(id))
            {
                return BadRequest();
            }

            if (!await houseService.IsUserOwnerByIdAsync(userId, id))
            {
                return Unauthorized();
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

            await houseService.EditHouseAsync(model, id);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.Id();
            if (!await agentService.IsExistByIdAsync(userId))
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            HouseDetailsViewModel? model = await houseService.GetHouseDetailsByIdAsync(id);
            if (model == null)
            {
                return BadRequest();
            }

            if (!await houseService.IsUserOwnerByIdAsync(userId, id))
            {
                return Unauthorized();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, HouseDetailsViewModel model)
        {
            string userId = User.Id();
            if (!await agentService.IsExistByIdAsync(userId))
            {
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (!await houseService.IsHouseExistById(id))
            {
                return BadRequest();
            }

            if (!await houseService.IsUserOwnerByIdAsync(userId, id))
            {
                return Unauthorized();
            }

            await houseService.DeleteHouseByIdAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if(!await houseService.IsHouseExistById(id))
            {
                return BadRequest();
            }

            if(await houseService.IsRentedByIdAsync(id))
            {
                return BadRequest();
            }

            string userId = User.Id();
            if(await agentService.IsExistByIdAsync(userId))
            {
                return Unauthorized();
            }

            await houseService.RentHouseByIdAsync(userId, id);

            return RedirectToAction(nameof(Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (!await houseService.IsHouseExistById(id) || 
                !await houseService.IsRentedByIdAsync(id))
            {
                return BadRequest();
            }

            string userId = User.Id();
            if(!await houseService.IsUserRentHouseByIdAsync(userId, id))
            {
                return Unauthorized();
            }

            await houseService.LeaveHouseAsync(id);

            return RedirectToAction(nameof(Mine));
        }
    }
}
