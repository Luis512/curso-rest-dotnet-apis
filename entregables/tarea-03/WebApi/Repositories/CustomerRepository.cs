using System;
using System.Linq;
using WebApi.Infrastructure.Data.Models;
using WebApi.ViewModels;

namespace WebApi.Repositories
{
    public class CustomerRepository
    {
        public AdventureworksContext UnitOfWork { get; set; }

        public CustomerRepository(AdventureworksContext context)
        {
            UnitOfWork = context;
        }

        public object[] Get()
        {
            return UnitOfWork.Customer
                .Select(c => new {
                    Id = c.CustomerId,
                    c.FirstName,
                    c.MiddleName,
                    c.LastName
                })
                .ToArray();
        }
        public object Get(int id)
        {
            var query = UnitOfWork.Customer
                .Where(x => x.CustomerId.Equals(id))
                .Select(c => new {
                    Id = c.CustomerId,
                    c.FirstName,
                    c.MiddleName,
                    c.LastName
                })
                .FirstOrDefault();
            return query;
        }

        public void Save(CustomerViewModel customer)
        {
            var query = UnitOfWork.Customer.AsQueryable();

            var model = new Customer
            {
                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName,
                PasswordHash = customer.PasswordHash,
                PasswordSalt = customer.PasswordSalt
            };
            UnitOfWork.Customer.Add(model);
            UnitOfWork.SaveChanges();
        }

        public bool Update(int id, CustomerViewModel request)
        {
            Customer model = UnitOfWork.Customer.Find(id);

            if (model == null)
                return false;
            model.FirstName = request.FirstName;
            model.MiddleName = request.MiddleName;
            UnitOfWork.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            UnitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Customer model = UnitOfWork.Customer.Find(id);
            if (model == null)
                return false;

            UnitOfWork.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            UnitOfWork.SaveChanges();
            return true;
        }
    }
}
