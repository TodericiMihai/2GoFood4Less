using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.User.CartObjects;
using _2GoFood4Less.Server.Models.Utils.Enums;
using _2GoFood4Less.Server.Services.CartServices.CartCommands.CartCommandsDto;


namespace _2GoFood4Less.Server.Services.CartServices.CartCommands
{
    public class CreateOrderFromCartCommand : ICartCommand
    {

        private readonly OrderLocationPayment _orderLocationPayment;

        public CreateOrderFromCartCommand(OrderLocationPayment orderLocationPayment)
        {
            _orderLocationPayment = orderLocationPayment;
        }
        public async Task Execute(Cart cart, ApplicationDbContext db)
        {
            try
            {
                if (cart == null || cart.CartItems.Count == 0)
                    return;

                var orderId = Guid.NewGuid().ToString();
                var payment = new Payment
                {
                    Id = Guid.NewGuid().ToString(),
                    PaymentMethod = _orderLocationPayment.PaymentMethod,
                    Value = _orderLocationPayment.Value,
                    OrderId = orderId 
                };
                var order = new Order
                {
                    Id = orderId ,
                    ClientId = cart.ClientId,
                    RestaurantId = cart.CartItems.First().MenuItem.Menu.RestaurantId,
                    UserSpecificRequests = cart.UserSpecificRequests,
                    Status = OrderStatus.Sent,
                    Location =_orderLocationPayment.Location,
                    Payment =payment
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
