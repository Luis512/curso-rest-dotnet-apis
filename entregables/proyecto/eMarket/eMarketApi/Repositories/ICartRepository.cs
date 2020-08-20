using eMarketDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApi.Repositories
{
    public interface ICartRepository
    {
        Task<bool> CheckoutCart(Cart cart);

        List<Order> GetPastOrders(string username);
    }
}
