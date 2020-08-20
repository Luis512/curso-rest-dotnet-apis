using eMarketDomain.Models;

namespace eMarketApi.Repositories
{
    public interface IUserRepository
    {
        public bool Login(Usuario user);

        public Usuario GetUserInformation(string username);
    }
}
