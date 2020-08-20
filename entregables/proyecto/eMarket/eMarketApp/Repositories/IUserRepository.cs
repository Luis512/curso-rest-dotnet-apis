using eMarketDomain.Models;
using System.Threading.Tasks;

namespace eMarketApp.Repositories
{
    public interface IUserRepository
    {
        Task<bool> LoginAsync(Usuario user);

        Task<Usuario> GetInformation(string username);
    }
}
