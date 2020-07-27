using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public ActionResult<IEnumerable<object>> Get()
        {
            var result = _repository.Get();
            _logger.LogInformation(_settings.Variable);
            return Ok(result);
        }

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
        public IActionResult Post([FromBody] string name)
        {
            _repository.Save(name);
            var items = _repository.Get();
            dynamic value = items.Last();
            return CreatedAtAction(nameof(GetById), new { id = value.Id }, value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new ObjectResult(new object()) { StatusCode = (int)HttpStatusCode.NotImplemented };
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return new ObjectResult(new object()) { StatusCode = (int)HttpStatusCode.NotImplemented };
        }


    }
}
