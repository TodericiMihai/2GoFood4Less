using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.UserObjects;
using System;

namespace _2GoFood4Less.Server.Services.CartServices.CartCommands
{
    public class ClearCartCommand : ICartCommand
    {
        public async Task Execute(Cart cart, ApplicationDbContext db)
        {
            try
            {
                db.Carts.Remove(cart);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error clearing cart: {ex.Message}", ex);
            }
        }

    }

}
