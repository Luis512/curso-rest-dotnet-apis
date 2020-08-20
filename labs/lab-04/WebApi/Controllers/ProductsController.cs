using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.ViewModels;

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
        private readonly ILogger<ProductsController> _logger;
        private readonly ApplicationSettings _settings;
        private readonly ProductRepository _repository;

        public ProductsController(ILogger<ProductsController> logger, ApplicationSettings settings, ProductRepository repository)
        {
            _logger = logger;
            _settings = settings;
            _repository = repository;
        }

        [HttpGet]
        [Produces(typeof(ProductViewModel))]
        

        [HttpGet("{id}")]
        public object GetById(int id)
        {
            var result = _repository.Get(id);
            if(result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductViewModel request)
        {
            _repository.Save(request);
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _repository.Delete(id);
            return result ? (IActionResult)Ok() : NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductViewModel request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = _repository.Update(id, request);
            return result ? (IActionResult)Ok() : NotFound();
        }


    }
}
