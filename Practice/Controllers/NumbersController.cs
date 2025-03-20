using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{
    public class NumbersController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Nums 1 ... 50";

            return View(50);
        }

        [HttpGet]
        public IActionResult NumbersToN(int num)
        {
            if (num < 1)
            {
                return View(nameof(Index), 5);
            }

            ViewBag.Title = $"Nums 1 ... {num}";

            return View("Index", num);
        }

        [HttpPost]
        public IActionResult NumbersToN(int num, string test)
        {
            if(num < 1)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Index), num);
        }
    }
}
