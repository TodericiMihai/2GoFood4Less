using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.UserObjects;
using _2GoFood4Less.Server.Services.CartServices.CartCalcualtion;
using Microsoft.EntityFrameworkCore;
using System;

namespace _2GoFood4Less.Server.Services.CartServices.CartCommands
{
    public class RemoveItemCommand : ICartCommand
    {
        private readonly string _menuItemId;
        private readonly ICartCalculator _calculator;

        public RemoveItemCommand(string menuItemId, ICartCalculator calculator)
        {
            _menuItemId = menuItemId;
            _calculator = calculator;
        }

        public async Task Execute(Cart cart, ApplicationDbContext db)
        {
            try
            {
                var item = cart.CartItems
                    .FirstOrDefault(ci => ci.MenuItemId == _menuItemId);

                if (item != null)
                    cart.CartItems.Remove(item);

                await _calculator.UpdateCartValue(cart, db);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error removing item from cart: {ex.Message}", ex);
            }
        }

    }


}
