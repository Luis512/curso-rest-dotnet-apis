using eMarketApi.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Constructor of <see cref="ProductController"/>
        /// </summary>
        /// <param name="productRepository">Dependecy injection of <see cref="IProductRepository"/></param>
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Gets all the products.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Product"/></returns>
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            return Ok(_productRepository.Get());
        }
               
        /// <summary>
        /// Gets a products by its id.
        /// </summary>
        /// <param name="id">Product's id</param>
        /// <returns>A <see cref="Product"/></returns>
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _productRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Gets a list of products by an specific category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ByCat/{id}")]
        public ActionResult<List<Product>> GetByCategory(int id)
        {
            return Ok(_productRepository.GetProductsByCategory(id));
        }

        /// <summary>
        /// Gets a list of products by an specific category.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ByOrder/{id}")]
        public ActionResult<List<Product>> GetByOrder(int id)
        {
            return Ok(_productRepository.GetProductsByOrder(id));
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">A <see cref="Product"/> to be created.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/></returns>
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {            
            var result = await _productRepository.Post(product);
            if (!result)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        /// <summary>
        /// Deletes a product by its id.
        /// </summary>
        /// <param name="id">Product's id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Task.Run(() => _productRepository.Delete(id));
            return Ok();
        }

        /// <summary>
        /// Updates an specific product.
        /// </summary>
        /// <param name="product">An updated <see cref="Product"/></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product product)
        {
           var result = await _productRepository.Put(product);
           if(!result)
           {
                return BadRequest();
           }

           return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
    }
}
