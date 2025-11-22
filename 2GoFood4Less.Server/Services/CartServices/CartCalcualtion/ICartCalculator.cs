using _2GoFood4Less.Server.Data;
using _2GoFood4Less.Server.Models.User.UserObjects;

namespace _2GoFood4Less.Server.Services.CartServices.CartCalcualtion
{
    public interface ICartCalculator
    {
        Task UpdateCartValue(Cart cart, ApplicationDbContext db);
    }

}
