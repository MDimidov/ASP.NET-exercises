using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TextSplitterApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TextSplitterApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(TextViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult SplitText(TextViewModel textModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), textModel);
            }

            return View(nameof(Index), textModel);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
