using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.RestaurantObjects
{
    public class MenuItem
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public MenuItemPhoto Photo { get; set; }

        public string MenuId { get; set; }

        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }


    }
}
