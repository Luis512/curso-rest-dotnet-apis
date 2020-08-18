using eMarketDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApp.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();

        Task<Product> GetProductById(int id);

        Task<List<Product>> GetProductsByCategory(int id);

        Task<List<Product>> GetProductsByOrder(int id);

        Task<bool> AddProduct(Product product);

        Task<bool> DeleteProduct(int id);
    }

}
