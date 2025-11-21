using System.ComponentModel.DataAnnotations;

namespace _2GoFood4Less.Server.Models.OrderObjects
{
    public class Order
    {

        [Key]
        public int Id { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } =new List<OrderItem>();

        public Payment payment { get; set; }

        public string UserSpecificRequests { get; set; }
    }
}
