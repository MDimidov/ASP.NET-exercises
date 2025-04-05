using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using Practice.Models.Home;

namespace Practice.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult About()
    {
        ViewData["Title"] = "About Page";
        ViewBag.Message = "This is an ASP.NET Core MVC app.";

        return View();
    }

    [HttpGet]
    public IActionResult GetNumber(decimal price)
    {
        string result = price.ToString();
        return Content(result);
    }

    [HttpGet]
    public IActionResult CheckDate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CheckDate(HomeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        return View();
    }

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
    {
        string path = Path.Combine(Environment.CurrentDirectory, "wwwroot", "Files");
        Directory.CreateDirectory(path);

        foreach (IFormFile file in files.Where(f => f.Length > 0))
        {
            string fileName = Path.Combine(path, file.FileName);

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        return Ok(new
        {
            savedFileLength = files.Sum(f => f.Length)
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
