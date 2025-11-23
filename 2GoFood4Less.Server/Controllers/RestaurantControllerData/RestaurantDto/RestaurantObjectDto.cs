namespace _2GoFood4Less.Server.Controllers.RestaurantControllerData.RestaurantDto
{
    public class RestaurantObjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodType { get; set; }
        public string ManagerId { get; set; }
        public List<MenuObjectDto> Menus { get; set; } = new();
    }
}
