using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using WebApi.Infrastructure.Data.Models;
using WebApi.Repositories;

namespace WebApi.Test
{
    public class CustomerTest
    {
        DbContextOptions<AdventureworksContext> options = new DbContextOptionsBuilder<AdventureworksContext>()
                                                            .UseInMemoryDatabase(databaseName: "AdventureworksContext")
                                                            .Options;
        [OneTimeSetUp]
        public void SetUP()
        {            
            using (var context = new AdventureworksContext(options))
            {
                List<Customer> customers = new List<Customer>();

                customers.Add(new Customer()
                {
                    CustomerId = 888,
                    FirstName = "Luis",
                    MiddleName = "Esteban",
                    LastName = "Alvarez"
                });
                customers.Add(new Customer()
                {
                    CustomerId = 456,
                    FirstName = "Marco",
                    MiddleName = "Antonio",
                    LastName = "Alvarezq"
                });
                customers.Add(new Customer()
                {
                    CustomerId = 789,
                    FirstName = "DELETE",
                    MiddleName = "DELETE",
                    LastName = "DELETE"
                });
                context.AddRange(customers);
                context.SaveChanges();
            }
        }

        [Test,Order(1)]
        public void CustomerController_GetById()
        {
            using (var context = new AdventureworksContext(options))
            {
                var repository = new CustomerRepository(context);
                var controller = new CustomerController(repository);
                var result = controller.Get(888);
                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(new
                {
                    Id = 888,
                    FirstName = "Luis",
                    MiddleName = "Esteban",
                    LastName = "Alvarez"
                });
            }
        }

        [Test, Order(2)]
        public void CustomerController_GetAllCustomer()
        {
            using (var context = new AdventureworksContext(options))
            {
                var repository = new CustomerRepository(context);
                var controller = new CustomerController(repository);
                var result = controller.Get();
                result.Should().NotBeNull();
                var response = result.Result as OkObjectResult;
                var customers = (IEnumerable<dynamic>)response.Value;
                response.StatusCode.Should().Be(200);             
                customers.Count().Should().Be(3);
            }
        }

        [Test, Order(3)]
        public void CustomerController_Delete()
        {
            using (var context = new AdventureworksContext(options))
            {
                var repository = new CustomerRepository(context);
                var controller = new CustomerController(repository);
                var result = controller.Delete(789);
                result.Should().NotBeNull();
                Assert.That(result, Is.InstanceOf<IActionResult>());
            }
        }
    }       
}
