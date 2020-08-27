using eMarketDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApi.Repositories
{
    public interface IProductRepository
    {
        List<Product> Get();

        Product Get(int id);

        List<Product> GetProductsByCategory(int id);

        List<Product> GetProductsByOrder(int id);

        Task<bool> Post(Product product);

        Task Delete(int id);

        Task<bool> Put(Product product);
    }
}
