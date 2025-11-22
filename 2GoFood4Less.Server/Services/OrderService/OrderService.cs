using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Services.OrderService.OrderCommands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;

        public OrderService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Order> ExecuteCommandAsync(string orderId, IOrderCommand command)
        {
            if (string.IsNullOrEmpty(orderId))
                throw new ArgumentException("OrderId cannot be null or empty.");

            try
            {
                var order = await GetByIdAsync(orderId);

                if (order == null)
                    throw new InvalidOperationException("Order not found.");

                await command.Execute(order, _db);
                await _db.SaveChangesAsync();

                return order;
            }
            catch (DbUpdateException dbEx)
            {
                Console.Error.WriteLine($"Database error: {dbEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        public async Task<Order> GetByIdAsync(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
                throw new ArgumentException("OrderId cannot be null or empty.");

            try
            {
                return await _db.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Include(o => o.Restaurant)
                    .Include(o => o.Client)
                    .FirstOrDefaultAsync(o => o.Id == orderId);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching order: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByClientAsync(string clientId)
        {
            try
            {
                return await _db.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Include(o => o.Restaurant)
                    .Where(o => o.ClientId == clientId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching client orders: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByRestaurantAsync(string restaurantId)
        {
            try
            {
                return await _db.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Include(o => o.Client)
                    .Where(o => o.RestaurantId == restaurantId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching restaurant orders: {ex.Message}");
                throw;
            }
        }
    }
}
