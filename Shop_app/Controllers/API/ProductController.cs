using Microsoft.AspNetCore.Mvc;
using Shop_app.Models;
using Shop_app.Services;

namespace Shop_app.Controllers.API
{
    //http://localhost:5247/api/product
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IServiceProducts _serviceProducts;
        public ProductController(IServiceProducts serviceProducts)
        {
            _serviceProducts = serviceProducts;
        }
        [HttpGet]
        public async Task<IActionResult> GetJsonAsync()
        {
            var products = await _serviceProducts.ReadAsync();
            if(products != null)
            {
                return Ok(products);
            }
            else
            {
                return NotFound(new { status = "404" });
            }
        }
        //http://localhost:5247/api/product/4
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _serviceProducts.GetByIdAsync(id);
            if(product != null)
            {
                return Ok(product);
            }
            return NotFound(new { status = "404" });
        }
        //Method: POST 
        //http://localhost:5247/api/product
        //Request:
        //Body
        //{
        //  Id: 0,
        //  Name: "A new Product",
        //  Price: 25,50,
        //  Description: "Some description"
        //}
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if(product != null)
            {
                var result = await _serviceProducts.CreateAsync(product);
                return Ok(product);
            }
            return BadRequest(new { state = "Product is null ..." });
        }
        //UpdateProduct
        //DeleteProduct

    }
}
