using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop_app.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        //http://localhost:[port]/user/register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Email or password are important ...");
            }
            //Create IdentityUser
            var newUser= new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(newUser, password);
            if(result.Succeeded)
            {
                return Ok($"User {newUser.UserName} is registered successfully!");
            }
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error);
            }
            return BadRequest(result.Errors);
        }
    }
}
