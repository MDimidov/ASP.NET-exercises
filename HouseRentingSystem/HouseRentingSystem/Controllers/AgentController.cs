using HouseRentingSystem.Attributes;
using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {

        private readonly IAgentService agentService;

        public AgentController(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        [HttpGet]
        [NotAnAgent]
        public IActionResult Become()
        {
            return View(new BecomeAgentFormModel());
        }

        [HttpPost]
        [NotAnAgent]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            string userId = User.Id();

            if (await agentService.IsExistByPhoneNumberAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber),
                    "Phone number already exist. Enter another phone.");
            }

            if (await agentService.IsUserHasRentsAsync(userId))
            {
                ModelState.AddModelError("Error", "You should have no rents to become an agent!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await agentService.CreateAsync(userId, model.PhoneNumber);

            return RedirectToAction(nameof(HouseController.Index), "House");
        }
    }
}
