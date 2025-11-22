using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;

namespace _2GoFood4Less.Server.Services.RestaurantMenuService.MenuCommands
{
    public interface IMenuCommand
    {
        Task Execute(Menu menu, ApplicationDbContext db);
    }
}
