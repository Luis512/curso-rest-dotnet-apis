using eMarketDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApp.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();

        Task<Category> GetCategoryById(int id);

        Task<bool> AddCategory(Category category);

        Task<bool> DeleteCategory(int id);
        
        Task<bool> UpdateCategory(Category category);
    }
}
