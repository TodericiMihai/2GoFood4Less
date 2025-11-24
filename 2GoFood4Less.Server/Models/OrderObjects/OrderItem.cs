using _2GoFood4Less.Server.Models.RestaurantObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _2GoFood4Less.Server.Models.OrderObjects
{
    public class OrderItem
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string MenuItemId { get; set; }  

        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }  

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string OrderId { get; set; }

        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Order Order { get; set; }


    }
}
