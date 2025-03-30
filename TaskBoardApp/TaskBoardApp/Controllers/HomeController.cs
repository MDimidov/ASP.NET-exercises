using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Contracts;
using TaskBoardApp.Models.Home;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBoardService boardService;

        public HomeController(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel model = await boardService.GetTasksCountAsync(GetUserId());

            return View(model);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
