using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_app.Services;

namespace Shop_app.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIOrderController : Controller
    {
        private readonly IOrderService _orderService;
        public APIOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {

        }
    }
}
