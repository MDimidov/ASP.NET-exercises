using Microsoft.AspNetCore.Mvc;
using ShoppingListApp.Contracts;
using ShoppingListApp.Models;

namespace ShoppingListApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await productService.GetAllProductsAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ProductViewModel model = new() { Name = string.Empty };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.CreateProductAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductViewModel model = await productService.GetProductByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.UpdateProductAsync(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteProductByIdAsync(id);

            return RedirectToAction(nameof(Index));
        }
    } 
}
