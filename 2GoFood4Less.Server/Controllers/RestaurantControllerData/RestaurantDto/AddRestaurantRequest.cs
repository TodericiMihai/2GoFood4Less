using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;

namespace _2GoFood4Less.Server.Controllers.RestaurantControllerData.RestaurantDto
{
    public class AddRestaurantRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodType { get; set; }
        public string ManagerId { get; set; }

    }
}
