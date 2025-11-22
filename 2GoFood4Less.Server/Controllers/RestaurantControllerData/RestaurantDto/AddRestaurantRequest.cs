using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;

namespace _2GoFood4Less.Server.Controllers.RestaurantControllerData.RestaurantDto
{
    public class AddRestaurantRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodType { get; set; }
        public RestaurantPhoto Photo { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime ClosingTime { get; set; }
    }
}
