using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskBoardApp.Contracts;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IBoardService boardService;
        private readonly ITaskService taskService;

        public TaskController(
            IBoardService boardService,
            ITaskService taskService)
        {
            this.boardService = boardService;
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel model = new();
            model.Boards = await boardService.GetBoardsListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!User.Identity!.IsAuthenticated)
            {
                RedirectToAction("Index", "Board");
            }

            await taskService.CreateTaskAsync(model, GetUserId());
            return RedirectToAction("Index", "Board");
        }

        public async Task<IActionResult> Details(int id)
        {
            TaskDetailsViewModel model = await taskService.GetTaskById(id);
            return View(model);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
