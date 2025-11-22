namespace _2GoFood4Less.Server.Controllers.CartControllerData.CartDto
{
    public class AddItemRequest
    {
        public string ClientId { get; set; }
        public string MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
