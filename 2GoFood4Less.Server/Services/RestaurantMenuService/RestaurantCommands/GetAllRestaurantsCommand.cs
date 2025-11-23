using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using Microsoft.EntityFrameworkCore;


namespace _2GoFood4Less.Server.Services.RestaurantServices.RestaurantCommands
{
    public class GetAllRestaurantsCommand : IRestaurantCommand
    {
        public async Task Execute(Restaurant unused, ApplicationDbContext db)
        {
            var restaurants = await db.Restaurants
                .Include(r => r.Menus)
                    .ThenInclude(m => m.Items)
                //.Include(r => r.Orders)
                .ToListAsync();

            // Same note: store result if needed
        }
    }
}
