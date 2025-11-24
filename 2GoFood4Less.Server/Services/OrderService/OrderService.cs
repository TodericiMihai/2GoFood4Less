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

        //public async Task<IEnumerable<Order>> GetOrdersByClientAsync(string clientId)
        //{
        //    try
        //    {
        //        return await _db.Orders
        //            .Include(o => o.OrderItems)
        //                .ThenInclude(oi => oi.MenuItem)
        //            .Include(o => o.Restaurant)
        //            .Where(o => o.ClientId == clientId)
        //            .ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine($"Error fetching client orders: {ex.Message}");
        //        throw;
        //    }
        //}

        //public async Task<IEnumerable<Order>> GetOrdersByRestaurantAsync(string restaurantId)
        //{
        //    try
        //    {
        //        return await _db.Orders
        //            .Include(o => o.OrderItems)
        //                .ThenInclude(oi => oi.MenuItem)
        //            .Include(o => o.Client)
        //            .Where(o => o.RestaurantId == restaurantId)
        //            .ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine($"Error fetching restaurant orders: {ex.Message}");
        //        throw;
        //    }
        //}

        public async Task<IEnumerable<OrderDto>> GetOrdersByClientAsync(string clientId)
        {
            try
            {
                var orders = await _db.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Include(o => o.Payment)
                    .Include(o => o.Restaurant)
                    .Where(o => o.ClientId == clientId)
                    .ToListAsync();

                // Map to DTOs
                return orders.Select(order => new OrderDto
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    RestaurantId = order.Restaurant.Id,
                    RestaurantName = order.Restaurant.Name,
                    Status = order.Status,
                    Location = order.Location,
                    PaymentMethod = order.Payment.PaymentMethod,
                    Value = order.Payment.Value,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.MenuItem.Id,
                        Name = oi.MenuItem.Name,
                        Quantity = oi.Quantity
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching client orders: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByRestaurantAsync(string restaurantId)
        {
            try
            {
                    var orders = await _db.Orders
                        .Include(o => o.OrderItems)
                            .ThenInclude(oi => oi.MenuItem)
                        .Include(o => o.Client)
                        .Include(o => o.Payment)
                        .Include(o => o.Restaurant)
                        .Where(o => o.RestaurantId == restaurantId)
                        .ToListAsync();

                // Map to DTOs
                return orders.Select(order => new OrderDto
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    RestaurantId = order.RestaurantId,
                    RestaurantName = order.Restaurant.Name,
                    Status = order.Status,
                    Location = order.Location,
                    PaymentMethod = order.Payment.PaymentMethod,
                    Value = order.Payment.Value,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.MenuItem.Id,
                        Name = oi.MenuItem.Name,
                        Quantity = oi.Quantity
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching restaurant orders: {ex.Message}");
                throw;
            }
        }

    }
}
