using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Contracts;
using TaskBoardApp.Models.Board;

namespace TaskBoardApp.Controllers
{
    public class BoardController : Controller
    {
        private readonly IBoardService boardService;

        public BoardController(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<BoardViewModel> model = await boardService.GetAllAsync();

            return View(model);
        }
    }
}
