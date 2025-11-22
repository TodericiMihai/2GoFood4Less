using System.Threading.Tasks;

namespace _2GoFood4Less.Server.Services
{
    public interface IService<T, TCommand>
    {
        Task<T> ExecuteCommandAsync(string id, TCommand command);
        Task<T> GetByIdAsync(string id);
    }
}
