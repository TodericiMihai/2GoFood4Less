using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Services.OrderService.OrderCommands;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<Order> ExecuteCommandAsync(string orderId, IOrderCommand command);
        Task<Order> GetByIdAsync(string orderId);

        Task<IEnumerable<Order>> GetOrdersByClientAsync(string clientId);
        Task<IEnumerable<Order>> GetOrdersByRestaurantAsync(string restaurantId);
    }
}
