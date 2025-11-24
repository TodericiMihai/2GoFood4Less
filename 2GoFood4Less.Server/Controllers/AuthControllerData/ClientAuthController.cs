using _2GoFood4Less.Server.Controllers.AuthControllerData.AuthDtoManager;
using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Infrastructure;
using _2GoFood4Less.Server.Models.AuthObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _2GoFood4Less.Server.Controllers.AuthControllerData
{
    [Route("api/client/auth")]
    [ApiController]
    public class ClientAuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenProvider _tokenProvider;

        public ClientAuthController(
            UserManager<AppUser> userManager,
            TokenProvider tokenProvider)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
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
            }
            catch (Exception ex)
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

                if (user is not Client client)
                    return Unauthorized("Invalid credentials.");

                // Verify password with UserManager
                bool passwordValid = await _userManager.CheckPasswordAsync(client, login.Password);
                if (!passwordValid)
                    return Unauthorized("Invalid credentials.");

                // Generate JWT token
                string token = _tokenProvider.Create(client);

                // Update last login
                client.LastLogin = DateTime.UtcNow;
                await _userManager.UpdateAsync(client);

                return Ok(new
                {
                    message = "Login successful.",
                    token
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
            return Ok(new { message = "Client logout successful. Discard the token on the client." });
        }

        // CURRENT USER -------------------------------------------------
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> GetCurrent()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is not Client client)
                return Unauthorized("User is not a client.");

            return Ok(new
            {
                client.Id,
                client.Name,
                client.Email,
                client.UserName,
                client.LastLogin
            });
        }
    }
}
