using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eMarketApi.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> Get();

        Category Get(int id);

        void Post(Category category);

        void Delete(int id);
    }
}
