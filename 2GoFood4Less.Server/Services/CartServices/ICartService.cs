using _2GoFood4Less.Server.Models.User.UserObjects;
using _2GoFood4Less.Server.Services.CartServices.CartCommands;

namespace _2GoFood4Less.Server.Services.CartServices
{
    public interface ICartService
    {
        Task<Cart> ExecuteCommandAsync(string clientId, ICartCommand command);
        Task<Cart> GetCartAsync(string clientId);

    }
}
