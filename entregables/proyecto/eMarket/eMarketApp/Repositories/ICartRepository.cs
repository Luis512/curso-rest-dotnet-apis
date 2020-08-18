using eMarketDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApp.Repositories
{
    public interface ICartRepository
    {
        Task<bool> Checkout(Cart cart);

        Task<List<Order>> GetPastOrders(string username);
    }
}
