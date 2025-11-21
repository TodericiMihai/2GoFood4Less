using System.ComponentModel.DataAnnotations.Schema;
using _2GoFood4Less.Server.Models.RestaurantObjects;

namespace _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects
{
    public class RestaurantPhoto : Photo
    {
        public string RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant Restaurant { get; set; }
    }
}
