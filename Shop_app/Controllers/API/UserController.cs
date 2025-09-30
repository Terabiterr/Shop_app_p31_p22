using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop_app.Models;

namespace Shop_app.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var newUser = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    return Ok(new { status = 200, message = "User register successfully" });
                }
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error);
                }
                return BadRequest(result.Errors);
            }
            else
            {
                return BadRequest(new { message = "Error model" });
            }
        }
        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null)
                {
                    return Unauthorized(new { message = "Invalid login attempt 1" }); 
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if(result.Succeeded)
                {
                    return Ok(new { message = "User logged in" });
                }
                return Unauthorized(new { message = "Invalid login attempt 2" });
            }
            else
            {
                return BadRequest(new { message = "Error model" });
            }
        }
    }
}
