using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    public class ProductRepository
    {
       private readonly List<string> ProductCatalog = new List<string>
       {
            "Jeans", "T-shirt", "Pants"
       };

        public object[] Get()
        {
            int i = 0;
            return ProductCatalog.Select(m => new
            {
                Name = m,
                Id = i++
            }).ToArray();
        }
        public object Get(int id)
        {
            int i = 0;
            var result = ProductCatalog.Select(m => new
            {
                Name = m,
                Id = i++
            }).ToList();

            return result.ElementAtOrDefault(id);
        }

        public  void Save(string name)
        {
            ProductCatalog.Add(name);
        }
    }

    
}