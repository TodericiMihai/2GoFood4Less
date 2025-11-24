using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Infrastructure;
using _2GoFood4Less.Server.Models.AuthObjects;
using Microsoft.AspNetCore.Identity;

namespace _2GoFood4Less.Server.Controllers.AuthControllerData.AuthDtoClient
{
    internal sealed class LoginUser(ApplicationDbContext context, PasswordHasher<AppUser> passwordHasher, UserManager<AppUser> userManager, TokenProvider tokenProvider)
    {
        public sealed record Request(string Email, string Password);

        public async Task<string> Handler(Request request)
        {
            AppUser? client = await userManager.FindByEmailAsync(request.Email);

            if (client is null) {
                throw new Exception("The user was not found");
            }

            var result = passwordHasher.VerifyHashedPassword(client,request.Password, client.PasswordHash);
            
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("The password is incorrect");
            }

            string token = tokenProvider.Create(client);

            return token;
        }
    }
}
