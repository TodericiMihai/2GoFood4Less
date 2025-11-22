using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Services.RestaurantServices.RestaurantCommands;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.RestaurantServices
{
    public class RestaurantService : IService<Restaurant, IRestaurantCommand>
    {
        private readonly ApplicationDbContext _db;

        public RestaurantService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Restaurant> ExecuteCommandAsync(string id, IRestaurantCommand command)
        {
            Restaurant restaurant = null;

            if (!string.IsNullOrEmpty(id))
            {
                restaurant = await GetByIdAsync(id);
            }

            await command.Execute(restaurant, _db);

            await _db.SaveChangesAsync();

            return restaurant;
        }

        public async Task<Restaurant> GetByIdAsync(string id)
        {
            return await _db.Restaurants
                .Include(r => r.Menus)
                    .ThenInclude(m => m.Items)
                .Include(r => r.Orders)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
