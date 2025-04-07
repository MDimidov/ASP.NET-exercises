using HouseRentingSystem.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
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
        public IActionResult Add()
        {
            return View(new HouseFormModel());
        }

        [HttpPost]
        public IActionResult Add(HouseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Index));
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
