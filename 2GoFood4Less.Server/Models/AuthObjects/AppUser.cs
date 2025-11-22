using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.AuthObjects
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "timestamp")]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "timestamp")]
        public DateTime LastLogin { get; set; } = DateTime.Now;

    }
}
