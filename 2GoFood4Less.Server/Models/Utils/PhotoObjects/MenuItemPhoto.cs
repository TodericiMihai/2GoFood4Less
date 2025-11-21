
using _2GoFood4Less.Server.Models.RestaurantObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects
{
    public class MenuItemPhoto: Photo
    { 
        public string MenuItemId { get; set; }

        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }
    }
}
