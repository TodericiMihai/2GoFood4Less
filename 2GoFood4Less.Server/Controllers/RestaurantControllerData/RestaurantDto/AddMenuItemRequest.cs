using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;

namespace _2GoFood4Less.Server.Controllers.RestaurantControllerData.RestaurantDto
{
    public class AddMenuItemRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
    }
}
