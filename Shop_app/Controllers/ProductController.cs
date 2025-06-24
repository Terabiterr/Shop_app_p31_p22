using Microsoft.AspNetCore.Mvc;
using Shop_app.Models;
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
        //Will add CRUD operations
        [HttpGet] //https://localhost:port/product/create
        public ViewResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken] // Validate the anti-forgery token for security
        // POST: http://localhost:[port]/products/create
        // Handle product creation form submission
        //Price is must have , => example 12,50
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description")] Product product)
        {
            if (ModelState.IsValid) // Check if the form data is valid
            {
                await _serviceProducts.CreateAsync(product); // Create the product asynchronously
                return RedirectToAction(nameof(Index)); // Redirect to the product list
            }
            return NotFound(); // If validation fails, return to the form with the entered data
        }
        //Will add CRUD operations
        [HttpGet] //https://localhost:port/product/update
        public ViewResult Update() => View();

        [HttpPost("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Price,Description")] Product product)
        {
            if(ModelState.IsValid)
            {
                await _serviceProducts.UpdateAsync(id, product);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
