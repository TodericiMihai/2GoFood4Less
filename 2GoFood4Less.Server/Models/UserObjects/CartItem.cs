using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using _2GoFood4Less.Server.Models.User.UserObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.UserObjects
{
    public class CartItem
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
        public string CartId { get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
    }
}
