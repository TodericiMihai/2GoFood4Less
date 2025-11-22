using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.User.UserObjects;


namespace _2GoFood4Less.Server.Services.CartServices.CartCommands
{
    public class CreateOrderFromCartCommand : ICartCommand
    {
        public async Task Execute(Cart cart, ApplicationDbContext db)
        {
            try
            {
                if (cart == null || cart.CartItems.Count == 0)
                    return;

                var order = new Order
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = cart.ClientId,
                    RestaurantId = cart.CartItems.First().MenuItem.Menu.RestaurantId,
                    UserSpecificRequests = cart.UserSpecificRequests,
                    Status = OrderStatus.Sent
                };

                foreach (var cartItem in cart.CartItems)
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        Id = Guid.NewGuid().ToString(),
                        MenuItemId = cartItem.MenuItemId,
                        Quantity = cartItem.Quantity,
                        OrderId = order.Id
                    });
                }

                db.Orders.Add(order);
                db.Carts.Remove(cart);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error creating order from cart: {ex.Message}", ex);
            }
        }

    }
}
