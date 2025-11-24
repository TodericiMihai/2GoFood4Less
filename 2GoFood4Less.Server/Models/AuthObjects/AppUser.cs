using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2GoFood4Less.Server.Models.AuthObjects
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }

        [Column(TypeName = "timestamptz")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "timestamptz")]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "timestamptz")]
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;

    }
}
