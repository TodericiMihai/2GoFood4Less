using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.RestaurantObjects;
using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services.RestaurantServices.RestaurantCommands
{
    public interface IRestaurantCommand
    {
        Task Execute(Restaurant unused, ApplicationDbContext db);
    }
}
