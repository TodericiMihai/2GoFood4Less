using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.User.UserObjects;

namespace _2GoFood4Less.Server.Services.OrderService.OrderCommands
{
    public interface IOrderCommand
    {
        Task Execute(Order order, ApplicationDbContext db);
    }
}
