using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.RestaurantServices.RestaurantCommands
{
    public class RemoveRestaurantCommand : IRestaurantCommand
    {
        private readonly string _restaurantId;

        public RemoveRestaurantCommand(string restaurantId)
        {
            _restaurantId = restaurantId;
        }

        public async Task Execute(Restaurant unused, ApplicationDbContext db)
        {
            try
            {
                var restaurant = await db.Restaurants
                    .Include(r => r.Menus)
                        .ThenInclude(m => m.Items)
                    .Include(r => r.Orders)
                    .FirstOrDefaultAsync(r => r.Id == _restaurantId);

                if (restaurant == null)
                    return;

                foreach (var order in restaurant.Orders.ToList())
                {
                    db.Orders.Remove(order);
                }

                db.Restaurants.Remove(restaurant);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing restaurant: {ex.Message}", ex);
            }
        }
    }
}
