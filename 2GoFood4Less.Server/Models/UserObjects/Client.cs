using _2GoFood4Less.Server.Models.OrderObjects;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.User.UserObjects
{
        public class Client : IdentityUser
        {

            [MaxLength(50)]
            public string Name { get; set; }
            
            [Column(TypeName = "timestamp")]
            public DateTime CreatedDate { get; set; } = DateTime.Now;

            [Column(TypeName = "timestamp")]
            public DateTime ModifiedDate { get; set; } = DateTime.Now;

            [Column(TypeName = "timestamp")]
            public DateTime LastLogin { get; set; } = DateTime.Now;

            public bool IsAdmin { get; set; } = false;
    
            public Cart Cart { get; set; }

            public ICollection<Order> Orders { get; set; } = new List<Order>();

        }
    
}
