using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WorldWhisper.Models;
using WorldWhisper.Models.Dtos;

namespace WorldWhisper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName=registerDto.Username,
                Email=registerDto.Email,
            };
            var result=await _userManager.CreateAsync(user,registerDto.Password);
            if (result.Succeeded)
            {
                var role = string.IsNullOrEmpty(registerDto.Role) ? "Customer" : registerDto.Role;

                if (!await _userManager.IsInRoleAsync(user, role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }

                return Ok(new { message = $"User registered with role: {role}" });


            }
            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user=await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user,model.Password))
            {
                return Ok(user);
            }
            return Unauthorized(new { error="Invalid credential" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok("User Sucessfully logged out");
        }
    }
}
