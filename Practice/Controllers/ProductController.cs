using Microsoft.AspNetCore.Mvc;
using Practice.Contracts;
using Practice.Models.Product;
using Practice.Services;
using System.Text.Json;

namespace Practice.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Cheese",
                Price = 20.0,
            },
            new ProductViewModel()
            {
                Id = 2,
                Name = "Ham",
                Price = 15.5,
            },
            new ProductViewModel()
            {
                Id = 3,
                Name = "Bread",
                Price = 1.5,
            },
        };

        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "All products";
            return View(products);
        }

        [HttpGet]
        public IActionResult ById(int id)
        {
            var product = productService.GetProductById(id, products);

            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Title = $"Product N.{product.Id}";
            return View(product);
        }

        [HttpGet]
        public IActionResult AllAsJson()
        {
            var allProducts = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            return Json(products, allProducts);
        }
    }
}
