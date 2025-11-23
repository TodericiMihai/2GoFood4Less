using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _2GoFood4Less.Server.Models.RestaurantObjects
{
    public class Menu
    {

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ?Description { get; set; }

        public string RestaurantId { get; set; }

        [JsonIgnore]
        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }

        public ICollection<MenuItem> Items { get; set; } = new List<MenuItem>();
    }
}
