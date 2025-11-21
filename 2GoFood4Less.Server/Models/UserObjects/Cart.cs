using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.UserObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.User.UserObjects
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public string ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public int Value { get; set; }

        public string UserSpecificRequests { get; set; }

    }
}
