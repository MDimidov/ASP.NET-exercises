using DeskMarket.Contracts;
using DeskMarket.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeskMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductViewModel> model = await productService.GetAllProductsAsync(GetUserId());

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {

            ProductAddFormModel model = new();
            model.Categories = await categoryService.GetAllForFormAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(ProductAddFormModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllForFormAsync();
                return View(model);
            }

            await productService.AddProductAsync(model, GetUserId());
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int id)
        {
            await productService.AddProductToCardAsync(id, GetUserId());

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            await productService.RemoveProductFromCardAsync(id, GetUserId());

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            IEnumerable<ProductCartViewModel> model = await productService.GetCartProductByUserIdAsync(GetUserId());
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = GetUserId();
            ProductEditFormModel? model = await productService.GetProductByIdAsync(id);

            if (model == null)
            {
                ModelState.AddModelError(nameof(model), "Invalid product");

                return RedirectToAction(nameof(Index));
            }

            if(model.SellerId != userId)
            {
                return Unauthorized();
            }

            model.Categories = await categoryService.GetAllForFormAsync();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(ProductEditFormModel model, int id)
        {
            if (model.SellerId != GetUserId())
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllForFormAsync();
                return View(model);
            }

            await productService.EditProductAsync(model, id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProductDetailsViewModel model = await productService.GetProductDetailsByIdAsync(id);

            return View(model);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
