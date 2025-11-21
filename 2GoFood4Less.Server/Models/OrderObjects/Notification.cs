using System.ComponentModel.DataAnnotations;

namespace _2GoFood4Less.Server.Models.OrderObjects
{
    public class Notification
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
