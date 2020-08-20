using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Infrastructure.Data.Models;
namespace WebApi.Test
{
    public class ProductTest
    {
        DbContextOptions<AdventureworksContext> options = new DbContextOptionsBuilder<AdventureworksContext>()
                                                    .UseInMemoryDatabase(databaseName: "eMarketDB")
                                                    .Options;
        [OneTimeSetUp]
        public void SetUP()
        {
            using (var context = new AdventureworksContext(options))
            {
                List<Product> customers = new List<Product>();

                customers.Add(new Product()
                {
                    ProductId = 123,
                    Name = "Pants",
                    ListPrice = 12,
                    Weight = 23
                });
                customers.Add(new Product()
                {
                    ProductId = 456,
                    Name = "Pants",
                    ListPrice = 12,
                    Weight = 23
                });
                customers.Add(new Product()
                {
                    ProductId = 789,
                    Name = "DELETE",
                    ListPrice = 12,
                    Weight = 23
                });
                context.AddRange(customers);
                context.SaveChanges();
            }
        }

        [Test]
        public void CustomerController_GetById()
        {
            using (var context = new AdventureworksContext(options))
            {
                var repository = new ProductRepository(context);
                var controller = new ProductsController(repository);
                var result = controller.GetById(123);
                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(new
                {
                    Name = "Pants",
                    ListPrice = 12,
                    Weight = 23
                });
            }
        }

        [Test]
        public void CustomerController_Delete()
        {
            using (var context = new AdventureworksContext(options))
            {
                var repository = new ProductRepository(context);
                var controller = new ProductsController(repository);
                var result = controller.Delete(789);
                result.Should().NotBeNull();
                Assert.That(result, Is.InstanceOf<IActionResult>());
            }
        }
    }
}