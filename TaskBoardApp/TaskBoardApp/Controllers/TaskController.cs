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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            TaskDetailsViewModel model = await taskService.GetTaskById(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            TaskFormModel model = await taskService.GetTaskFormById(id, GetUserId());
            model.Boards = await boardService.GetBoardsListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskFormModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!(await boardService.GetBoardsListAsync()).Any(b => b.Id == id))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            await taskService.EditTaskAsync(model, id, GetUserId());

            return RedirectToAction("Index", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            TaskViewModel model = await taskService.GetTaskDeleteByIdAsync(id, GetUserId());

            if(model.Owner != GetUserId())
            {
                return Unauthorized();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel model)
        {
            TaskViewModel modelToDelete = await taskService.GetTaskDeleteByIdAsync(model.Id, GetUserId());

            if (modelToDelete.Owner != GetUserId())
            {
                return Unauthorized();
            }

            await taskService.DeleteTaskByIdAsync(modelToDelete.Id);

            return RedirectToAction("Index", "Board");
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
