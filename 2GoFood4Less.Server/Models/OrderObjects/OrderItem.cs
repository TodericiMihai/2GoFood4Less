using _2GoFood4Less.Server.Models.RestaurantObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.OrderObjects
{
    public class OrderItem
    {
        [Key]
        public string id { get; set; }

        [Required]
        public string MenuItemId { get; set; }  

        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }  

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

    }
}
