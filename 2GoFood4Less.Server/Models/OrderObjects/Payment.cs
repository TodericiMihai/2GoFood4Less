using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using _2GoFood4Less.Server.Models.Utils.Enums;
namespace _2GoFood4Less.Server.Models.OrderObjects
{
    public class Payment
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public int Value { get; set; }

        public string OrderId { get; set; }

        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Order Order { get; set; }

    }
}
