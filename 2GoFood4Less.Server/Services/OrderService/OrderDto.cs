using _2GoFood4Less.Server.Models.OrderObjects;

namespace _2GoFood4Less.Server.Services.OrderService
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string RestaurantId { get; set; }  
        public string RestaurantName { get; set; }
        public string Value { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
