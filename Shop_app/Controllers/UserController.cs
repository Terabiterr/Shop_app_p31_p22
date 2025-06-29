using Microsoft.AspNetCore.Mvc;

namespace Shop_app.Controllers
{
    public class UserController : Controller
    {
        //http://localhost:port/user/register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
