using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Practice.Contracts;
using Practice.Models.Product;
using Practice.Services;
using System.Net.Mime;
using System.Text;
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
        public IActionResult All(string keyword = "")
        {
            ViewBag.Title = $"All products with match \"{keyword}\"";

            var matchedProducts = products
                .Where(p =>
                    p.Name.ToLower().Contains(keyword.ToLower()));

            return View(nameof(Index), matchedProducts);
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

        [HttpGet]
        public IActionResult AllAsText()
        {
            return Content(productService.GetAllProductsAsPlainText(products));
        }

        [HttpGet]
        public IActionResult AllAsTextFile()
        {
            Response.Headers.Append(HeaderNames.ContentDisposition, @"attachment;filename=products.txt");
            return File(Encoding.UTF8.GetBytes(productService.GetAllProductsAsPlainText(products)), "text/plain");
        }
    }
}
