using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.User.CartObjects;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.AuthObjects
{
        public class Client : AppUser
        {
            public Cart ?Cart { get; set; }

            public ICollection<Order> ?Orders { get; set; } = new List<Order>();

        }
    
}
