namespace _2GoFood4Less.Server.Controllers.RestaurantControllerData.RestaurantDto
{
    public class MenuObjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MenuItemObjectDto> Items { get; set; } = new();
    }
}
