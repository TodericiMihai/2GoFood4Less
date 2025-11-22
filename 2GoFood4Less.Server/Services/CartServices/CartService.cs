using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.CartObjects;
using _2GoFood4Less.Server.Services.CartServices.CartCommands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _db;

        public CartService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Cart> ExecuteCommandAsync(string clientId, ICartCommand command)
        {
            try
            {
                var cart = await GetCartAsync(clientId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        Id = Guid.NewGuid().ToString(),
                        ClientId = clientId
                    };
                    _db.Carts.Add(cart);
                }

                await command.Execute(cart, _db);

                await _db.SaveChangesAsync();
                return cart;
            }
            catch (Exception ex)
            {
                // You can log the exception here if you have a logger
                throw new ApplicationException($"Error executing command on cart for client '{clientId}': {ex.Message}", ex);
            }
        }

        public async Task<Cart> GetCartAsync(string clientId)
        {
            try
            {
                return await _db.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.MenuItem)
                    .FirstOrDefaultAsync(c => c.ClientId == clientId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving cart for client '{clientId}': {ex.Message}", ex);
            }
        }
    }
}
