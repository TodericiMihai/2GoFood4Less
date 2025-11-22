using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.CartObjects;
using _2GoFood4Less.Server.Services.CartServices.CartCalcualtion;
using System;

namespace _2GoFood4Less.Server.Services.CartServices.CartCommands
{
    public class UpdateQuantityCommand : ICartCommand
    {
        private readonly string _menuItemId;
        private readonly int _newQuantity;
        private readonly ICartCalculator _calculator;

        public UpdateQuantityCommand(string menuItemId, int newQuantity, ICartCalculator calculator)
        {
            _menuItemId = menuItemId;
            _newQuantity = newQuantity;
            _calculator = calculator;
        }

        public async Task Execute(Cart cart, ApplicationDbContext db)
        {
            try
            {
                var item = cart.CartItems
                    .FirstOrDefault(ci => ci.MenuItemId == _menuItemId);

                if (item == null)
                    return;

                if (_newQuantity <= 0)
                    cart.CartItems.Remove(item);
                else
                    item.Quantity = _newQuantity;

                await _calculator.UpdateCartValue(cart, db);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating quantity in cart: {ex.Message}", ex);
            }
        }


    }

}
