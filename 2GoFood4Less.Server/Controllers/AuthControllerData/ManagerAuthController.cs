using _2GoFood4Less.Server.Controllers.AuthControllerData.AuthDtoManager;
using _2GoFood4Less.Server.Infrastructure;
using _2GoFood4Less.Server.Models.AuthObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _2GoFood4Less.Server.Controllers.AuthControllerData
{
    [Route("api/manager/auth")]
    [ApiController]
    public class ManagerAuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenProvider _tokenProvider;

        public ManagerAuthController(
            UserManager<AppUser> userManager,
            TokenProvider tokenProvider)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
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
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user is not Manager manager)
                    return Unauthorized("Invalid credentials.");

                // Verify password with UserManager
                bool passwordValid = await _userManager.CheckPasswordAsync(manager, login.Password);
                if (!passwordValid)
                    return Unauthorized("Invalid credentials.");

                // Generate JWT token
                string token = _tokenProvider.Create(manager);

                // Update last login
                manager.LastLogin = DateTime.UtcNow;
                await _userManager.UpdateAsync(manager);

                return Ok(new
                {
                    message = "Manager login successful.",
                    token,
                    user = new
                    {
                        id = manager.Id,
                        name = manager.Name,
                        email = manager.Email
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Something went wrong: " + ex.Message });
            }
        }

        // LOGOUT -------------------------------------------------------
        [Authorize]
        [HttpGet("logout")]
        public ActionResult Logout()
        {
            // With JWT, logout is client-side: just discard the token
            return Ok(new { message = "Manager logout successful. Discard the token on the client." });
        }

        // CURRENT USER -------------------------------------------------
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> GetCurrent()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is not Manager manager)
                return Unauthorized("User is not a manager.");

            return Ok(new
            {
                manager.Id,
                manager.Name,
                manager.Email,
                manager.UserName,
                manager.LastLogin
            });
        }
    }
}