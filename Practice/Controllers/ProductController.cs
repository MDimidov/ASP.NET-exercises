using Microsoft.AspNetCore.Mvc;
using Practice.Models.Product;

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

        public IActionResult Index()
        {
            ViewBag.Title = "All products";
            return View(products);
        }
    }
}
