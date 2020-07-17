using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] ProductCatalog = new[]
        {
            "Jeans", "T-shirt", "Pants"
        };

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            int i = 0;
            var result = ProductCatalog.Select(m => new
            {
                Name = m,
                Id = i++
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public object GetById(int id)
        {
            int i = 0;
            var result = ProductCatalog.Select(m => new
            {
                Name = m,
                Id = i++
            }).ToList();

            if(result.ElementAtOrDefault(id) == null)
            {
                return NotFound();
            }
            return result[id];
        }
    }
}
