using _2GoFood4Less.Server.Controllers.AuthControllerData.AuthDto;
using _2GoFood4Less.Server.Models.AuthObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2GoFood4Less.Server.Controllers.AuthControllerData
{
    [Route("api/client/auth")]
    [ApiController]
    public class ClientAuthController : ControllerBase
    {
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;

        public ClientAuthController(
            UserManager<Client> userManager,
            SignInManager<Client> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // REGISTER -----------------------------------------------------
        [HttpPost("register")]
        public async Task<ActionResult> Register(Client user)
        {
            try
            {
                var newClient = new Client
                {
                    Name = user.Name,
                    Email = user.Email,
                    UserName = user.UserName,
                };

                var result = await _userManager.CreateAsync(newClient, user.PasswordHash);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

            }catch (Exception ex)
            {
                return BadRequest("Something went wrong, please try again. " + ex.Message);
            }

            return Ok(new { message = "Client registered successfully." });
        }

        // LOGIN --------------------------------------------------------
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user == null) return Unauthorized("Invalid credentials.");

                var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.Remember, false);

                if (!result.Succeeded)
                    return Unauthorized("Invalid credentials.");

                user.LastLogin = DateTime.Now;

                await _userManager.UpdateAsync(user);

            }catch (Exception ex)
            {
                return BadRequest(new { message = "Something went wrong, please try again. " + ex.Message });
            }
            return Ok(new { message = "Client login successful." });
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
            return Ok(new { message = "Client logged out." });
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
