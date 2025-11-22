using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.UserObjects;
using System;

namespace _2GoFood4Less.Server.Services.CartServices
{
    public interface ICartCommand
    {
        Task Execute(Cart cart, ApplicationDbContext db);
    }
}
