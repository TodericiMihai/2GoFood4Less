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
        private readonly RestaurantPhoto _photo;
        private readonly DateTime _openTime;
        private readonly DateTime _closingTime;

        public AddRestaurantCommand(string name, string description, string foodType,
            RestaurantPhoto photo, DateTime openTime, DateTime closingTime)
        {
            _name = name;
            _description = description;
            _foodType = foodType;
            _photo = photo;
            _openTime = openTime;
            _closingTime = closingTime;
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
                    Photo = _photo,
                    OpenTime = _openTime,
                    ClosingTime = _closingTime
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
