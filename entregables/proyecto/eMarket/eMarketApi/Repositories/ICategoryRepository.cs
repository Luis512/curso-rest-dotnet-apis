using eMarketDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApi.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> Get();

        Category Get(int id);

        Task<bool> Post(Category category);

        Task<bool> Delete(int id);

        Task<bool> Put(Category category);
    }
}
