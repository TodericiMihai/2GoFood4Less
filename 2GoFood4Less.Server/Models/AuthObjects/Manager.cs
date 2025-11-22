using _2GoFood4Less.Server.Models.RestaurantObjects;

namespace _2GoFood4Less.Server.Models.AuthObjects
{
    public class Manager : AppUser
    {
        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}
