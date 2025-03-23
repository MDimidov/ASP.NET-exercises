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
    }
}
