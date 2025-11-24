using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.CartObjects;
using _2GoFood4Less.Server.Models.UserObjects;
using _2GoFood4Less.Server.Services.CartServices.CartCalcualtion;

namespace _2GoFood4Less.Server.Services.CartServices.CartCommands
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

            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new ApplicationException($"Error adding item to cart: {ex.Message}", ex);
            }
        }



    }

}
