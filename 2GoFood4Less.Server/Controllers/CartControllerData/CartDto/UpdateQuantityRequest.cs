namespace _2GoFood4Less.Server.Controllers.CartControllerData.CartDto
{
    public class UpdateQuantityRequest
    {
        public string ClientId { get; set; }
        public string MenuItemId { get; set; }
        public int NewQuantity { get; set; }
    }
}
