using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;
using System;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.RestaurantServices.RestaurantCommands
{
    public class AddRestaurantCommand : IRestaurantCommand
    {
        private readonly string _name;
        private readonly string _description;
        private readonly string _foodType;
        private readonly string _managerId;


        public AddRestaurantCommand(string name, string description, string foodType,
           string managerId)
        {
            _name = name;
            _description = description;
            _foodType = foodType;
            _managerId = managerId;

        }

        public async Task Execute(Restaurant unused, ApplicationDbContext db)
        {
            try
            {
                var restaurant = new Restaurant
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = _name,
                    Description = _description,
                    FoodType = _foodType,
                    ManagerId = _managerId,
                };

                db.Restaurants.Add(restaurant);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding restaurant: {ex.Message}", ex);
            }
        }
    }
}
