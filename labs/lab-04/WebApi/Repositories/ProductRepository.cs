using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Infrastructure.Data.Models;
using WebApi.ViewModels;

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
            return UnitOfWork.Product
                .Select(p => new { 
                    Name = p.Name, 
                    p.ListPrice,
                    Category = new { p.ProductCategory.Name },
                    p.Weight
                })
                .ToArray();
        }
        public object Get(int id)
        {
            var query =  UnitOfWork.Product
                .Where(x =>x.ProductId.Equals(id))
                .Select(p => new {
                    Name = p.Name,
                    p.ListPrice,
                    Category = new { p.ProductCategory },
                    p.Weight
                })
                .FirstOrDefault();
            return query;
        }

        public  void Save(ProductViewModel product)
        {
            var query = UnitOfWork.Product.AsQueryable();
            var next = query.Max(p => p.ProductNumber) + 1;

            var model = new Product { 
                Name = product.Name, 
                SellStartDate = DateTime.Now, 
                SellEndDate = DateTime.Now.AddYears(1),
                ProductNumber = next,
            };
            UnitOfWork.Product.Add(model);
            UnitOfWork.SaveChanges();
        }

        public bool Update(int id, ProductViewModel request)
        {
            Product model = UnitOfWork.Product.Find(id);

            if (model == null)
                return false;
            model.ListPrice = request.ListPrice;
            model.Name = request.Name;
            model.Weight = request.Weight;

            UnitOfWork.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Delete (int id)
        {
            Product model = UnitOfWork.Product.Find(id);
            if (model == null)
                return false;     

            UnitOfWork.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            UnitOfWork.SaveChanges();
            return true;
        }
    }

    
}