using HouseRentingSystem.Core.Models.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class AgentController : Controller
    {
        [HttpGet]
        public IActionResult Become()
        {
            return View(new BecomeAgentFormModel());
        }

        [HttpPost]
        public IActionResult Become(BecomeAgentFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(HouseController.Index), nameof(HouseController));
        }
    }
}
