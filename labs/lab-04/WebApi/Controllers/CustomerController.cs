using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Repositories;
using WebApi.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _repository;

        public CustomerController(CustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            var result = _repository.Get();            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public object Get(int id)
        {
            var result = _repository.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        // POST api/<ClientsController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerViewModel request)
        {
            _repository.Save(request);
            return CreatedAtAction(nameof(Get), new { id = request.Id }, request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _repository.Delete(id);
            return result ? (IActionResult)Ok() : NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = _repository.Update(id, request);
            return result ? (IActionResult)Ok() : NotFound();
        }
    }
}
