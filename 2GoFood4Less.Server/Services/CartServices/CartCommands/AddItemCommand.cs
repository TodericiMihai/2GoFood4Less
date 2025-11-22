using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.UserObjects;
using _2GoFood4Less.Server.Models.UserObjects;
using _2GoFood4Less.Server.Services.CartServices.CartCalcualtion;

namespace _2GoFood4Less.Server.Services.CartServices.CartCommands
{
    public class AddItemCommand : ICartCommand
    {
        private readonly string _menuItemId;
        private readonly int _quantity;
        private readonly ICartCalculator _calculator;

        public AddItemCommand(string menuItemId, int quantity, ICartCalculator calculator)
        {
            _menuItemId = menuItemId;
            _quantity = quantity;
            _calculator = calculator;
        }

        public async Task Execute(Cart cart, ApplicationDbContext db)
        {
            try
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

                await _calculator.UpdateCartValue(cart, db);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new ApplicationException($"Error adding item to cart: {ex.Message}", ex);
            }
        }



    }

}
