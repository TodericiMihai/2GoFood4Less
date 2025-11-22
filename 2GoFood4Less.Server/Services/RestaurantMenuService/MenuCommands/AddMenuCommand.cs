using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Services.RestaurantMenuService.MenuCommands;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.MenuServices.MenuCommands
{
    public class AddMenuCommand : IMenuCommand
    {
        private readonly string _name;
        private readonly string _description;
        private readonly string _restaurantId;

        public AddMenuCommand(string name, string description, string restaurantId)
        {
            _name = name;
            _description = description;
            _restaurantId = restaurantId;
        }

        public async Task Execute(Menu unusedMenu, ApplicationDbContext db)
        {
            try
            {
                var restaurant = await db.Restaurants.FindAsync(_restaurantId);
                if (restaurant == null)
                    throw new ArgumentException("Restaurant not found");

                var newMenu = new Menu
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = _name,
                    Description = _description,
                    RestaurantId = _restaurantId,
                    Restaurant = restaurant
                };

                restaurant.Menus.Add(newMenu);
                db.Menus.Add(newMenu);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding menu: {ex.Message}", ex);
            }
        }
    }
}
