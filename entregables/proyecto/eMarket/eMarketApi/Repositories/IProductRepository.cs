using eMarketDomain.Models;
using System.Collections.Generic;

namespace eMarketApi.Repositories
{
    public interface IProductRepository
    {
        List<Product> Get();

        Product Get(int id);

        List<Product> GetProductsByCategory(int id);

        void Post(Product product);

        void Delete(int id);
    }
}
