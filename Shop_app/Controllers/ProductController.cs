using Microsoft.AspNetCore.Mvc;
using Shop_app.Services;

namespace Shop_app.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServiceProducts _serviceProducts;
        public ProductController(IServiceProducts serviceProducts)
        {
            _serviceProducts = serviceProducts;
        }

        public async Task<ViewResult> Index()
        {
            var products = await _serviceProducts.ReadAsync();
            return View(products);
        }
    }
}
