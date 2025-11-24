using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.Utils.Enums;
using System;

namespace _2GoFood4Less.Server.Services.OrderService.OrderCommands
{
    public class SetOrderFinishedCommand : IOrderCommand
    {
        public async Task Execute(Order order, ApplicationDbContext db)
        {
            try
            {
                if (order == null) return;
                order.Status = OrderStatus.Finished;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error setting order to Finished: {ex.Message}");
                throw;
            }
        }
    }
}
