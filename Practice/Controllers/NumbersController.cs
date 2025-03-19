using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{
    public class NumbersController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Tile = "Nums 1 ... 50";

            return View(50);
        }
    }
}
