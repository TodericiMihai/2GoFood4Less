using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using Microsoft.EntityFrameworkCore;


namespace _2GoFood4Less.Server.Services.RestaurantServices.RestaurantCommands
{
    public class GetRestaurantsByManagerCommand : IRestaurantCommand
    {
        public string ManagerId { get; }

        public GetRestaurantsByManagerCommand(string managerId)
        {
            ManagerId = managerId;
        }

        public async Task Execute(Restaurant unused, ApplicationDbContext db)
        {
            // Just for pattern compliance: store the result in unused if needed
            var restaurants = await db.Restaurants
                .Include(r => r.Menus)
                    .ThenInclude(m => m.Items)
                //.Include(r => r.Orders)
                .Where(r => r.ManagerId == ManagerId)
                .ToListAsync();

            // If you want, you can somehow attach this result to 'unused' or return via service
        }
    }
}
