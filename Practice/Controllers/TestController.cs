using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
