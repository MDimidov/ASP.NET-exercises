using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<PostModel> model = await postService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            PostModel model = new();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await postService.AddNewPostAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PostModel? model = await postService.GetPostByIdAsync(id);

            if (model == null)
            {
                ModelState.AddModelError("All", "Invalid Post");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await postService.EditPostAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await postService.DeletePostById(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
