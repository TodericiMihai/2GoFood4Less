using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.CartObjects;
using _2GoFood4Less.Server.Services.CartServices.CartCalcualtion;
using Microsoft.EntityFrameworkCore;
using System;

namespace _2GoFood4Less.Server.Services.CartServices.CartCommands
{
    public class RemoveItemCommand : ICartCommand
    {
        private readonly string _menuItemId;

        public RemoveItemCommand(string menuItemId)
        {
            _menuItemId = menuItemId;

        }

        public async Task Execute(Cart cart, ApplicationDbContext db)
        {
            try
            {
                var item = cart.CartItems
                    .FirstOrDefault(ci => ci.MenuItemId == _menuItemId);

                if (item != null)
                    cart.CartItems.Remove(item);

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error removing item from cart: {ex.Message}", ex);
            }
        }

    }


}
