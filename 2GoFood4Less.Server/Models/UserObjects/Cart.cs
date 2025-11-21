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

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Payment Payment { get; set; }

        public string UserSpecificRequests { get; set; }

    }
}
