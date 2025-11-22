using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Models.User.UserObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.OrderObjects
{
    public class Order
    {

        [Key]
        public string Id { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } =new List<OrderItem>();

        public string RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }

        public string ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public Payment Payment { get; set; }

        public string UserSpecificRequests { get; set; }

        public OrderStatus Status { get; set; }
    }
}
