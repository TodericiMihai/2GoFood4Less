using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.OrderObjects;
using System;

namespace _2GoFood4Less.Server.Services.OrderService.OrderCommands
{
    public class SetOrderProcessingCommand : IOrderCommand
    {
        public async Task Execute(Order order, ApplicationDbContext db)
        {
            try
            {
                if (order == null) return;
                order.Status = OrderStatus.Processing;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error setting order to Processing: {ex.Message}");
                throw;
            }
        }
    }
}
