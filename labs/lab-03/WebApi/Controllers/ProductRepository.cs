﻿using System.Collections.Generic;
using System.Linq;
using WebApi.Infrastructure.Data.Models;

namespace WebApi.Controllers
{
    public class ProductRepository
    {
        public AdventureworksContext UnitOfWork { get; set; }

        public ProductRepository(AdventureworksContext context)
        {
            UnitOfWork = context;
        }

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