using _2GoFood4Less.Server.Controllers.AuthControllerData.AuthDto;
using _2GoFood4Less.Server.Models.AuthObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2GoFood4Less.Server.Controllers.AuthControllerData
{
    [Route("api/manager/auth")]
    [ApiController]
    public class ManagerAuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ManagerAuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // REGISTER -----------------------------------------------------
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            try
            {
                var manager = new Manager
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    UserName = dto.UserName,
                };

                var result = await _userManager.CreateAsync(manager, dto.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok(new { message = "Manager registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // LOGIN --------------------------------------------------------
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            Manager manager = null;
            try
            {
                manager = (Manager)await _userManager.FindByEmailAsync(login.Email);
                if (manager == null) return Unauthorized("Invalid credentials.");

                var result = await _signInManager.PasswordSignInAsync(manager, login.Password, login.Remember, false);

                if (!result.Succeeded)
                    return Unauthorized("Invalid credentials.");

                manager.LastLogin = DateTime.Now;
                await _userManager.UpdateAsync(manager);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Something went wrong, please try again. " + ex.Message });
            }

            return Ok(new
            {
                message = "Manager login successful.",
                user = new
                {
                    id = manager.Id,
                    name = manager.Name,
                    email = manager.Email
                }
            });

        }

        // LOGOUT -------------------------------------------------------
        [Authorize]
        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Someting went wrong, please try again. " + ex.Message });
            }
            return Ok(new { message = "Manager logged out." });
        }

        // CURRENT USER -------------------------------------------------
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> GetCurrent()
        {
            var principal = User;
            var user = await _userManager.GetUserAsync(principal);
            return Ok(user);
        }
    }
}
