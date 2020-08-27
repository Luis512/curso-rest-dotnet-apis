using eMarketDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApi.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> Get();

        Category Get(int id);

        void Post(Category category);

        void Delete(int id);

        Task<bool> Put(Category category);
    }
}
