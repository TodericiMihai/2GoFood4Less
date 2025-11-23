using _2GoFood4Less.Server.Models.AuthObjects;
using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.Utils.Photo.PhotoObjects;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.RestaurantObjects
{
    public class Restaurant 
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string ?Description { get; set; }

        public RestaurantPhoto ?Photo { get; set; }

        public string ?FoodType { get; set; }//maybe i shoud make it a list

        [Column(TypeName = "time")]
        public TimeSpan ?OpenTime { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan ?ClosingTime { get; set; } 

        public string ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Manager Manager {  get; set; }
        
        public ICollection<Menu> ?Menus { get; set; } = new List<Menu>();

        public ICollection<Order> ?Orders { get; set; } = new List<Order>();

        public int ToPay => Orders.Count * 2;


    }
}
