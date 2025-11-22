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
        private readonly UserManager<Manager> _userManager;
        private readonly SignInManager<Manager> _signInManager;

        public ManagerAuthController(
            UserManager<Manager> userManager,
            SignInManager<Manager> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // REGISTER -----------------------------------------------------
        [HttpPost("register")]
        public async Task<ActionResult> Register(Manager manager)
        {
            try
            {
                    var newManager = new Manager
                {
                    Name = manager.Name,
                    Email = manager.Email,
                    UserName = manager.UserName,
                };

                var result = await _userManager.CreateAsync(newManager, manager.PasswordHash);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong, please try again. " + ex.Message);
            }

            return Ok(new { message = "Manager registered successfully." });
        }

        // LOGIN --------------------------------------------------------
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            try
            {
                var manager = await _userManager.FindByEmailAsync(login.Email);
                if (manager == null) return Unauthorized("Invalid credentials.");

                var result = await _signInManager.PasswordSignInAsync(manager, login.Password, login.Remember, false);

                if (!result.Succeeded)
                    return Unauthorized("Invalid credentials.");

                manager.LastLogin = DateTime.Now;
                await _userManager.UpdateAsync(manager);

            }catch (Exception ex)
            {
                return BadRequest(new { message = "Something went wrong, please try again. " + ex.Message });
            }

            return Ok(new { message = "Manager login successful." });
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
