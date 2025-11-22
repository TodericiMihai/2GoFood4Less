using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.UserObjects;
using _2GoFood4Less.Server.Models.UserObjects;
using Microsoft.EntityFrameworkCore;
using System;

namespace _2GoFood4Less.Server.Services.CartServices
{
    public class AddItemCommand : ICartCommand
    {
        private readonly string _menuItemId;
        private readonly int _quantity;

        public AddItemCommand(string menuItemId, int quantity)
        {
            _menuItemId = menuItemId;
            _quantity = quantity;
        }

        public async Task Execute(Cart cart, ApplicationDbContext db)
        {
            var existing = cart.CartItems
                .FirstOrDefault(ci => ci.MenuItemId == _menuItemId);

            if (existing != null)
            {
                existing.Quantity += _quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    id = Guid.NewGuid().ToString(),
                    MenuItemId = _menuItemId,
                    Quantity = _quantity,
                    CartId = cart.Id
                });
            }

            // OPTIONAL: update cart total value
            await UpdateCartValue(cart, db);
        }

        private async Task UpdateCartValue(Cart cart, ApplicationDbContext db)
        {
            cart.Value = await db.CartItems
                .Where(ci => ci.CartId == cart.Id)
                .SumAsync(ci => ci.Quantity * ci.MenuItem.Price); // if you have Price
        }
    }

}
