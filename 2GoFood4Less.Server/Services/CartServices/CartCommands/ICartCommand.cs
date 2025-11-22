using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.CartObjects;
using System;

namespace _2GoFood4Less.Server.Services.CartServices.CartCommands
{
    public interface ICartCommand
    {
        Task Execute(Cart cart, ApplicationDbContext db);
    }
}
