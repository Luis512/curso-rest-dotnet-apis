using System.Collections.Generic;
using System.Threading.Tasks;
using eMarketApi.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace eMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Controller that controlls the category section.
        /// </summary>
        /// <param name="categoryRepository">Dependency injection of <see cref="ICategoryRepository"/></param>
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Gets all the categories.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Category"/></returns>
        [HttpGet]
        public ActionResult<List<Category>> Get()
        {            
            return Ok(_categoryRepository.Get());
        }

        /// <summary>
        /// Gets a single category by its id.
        /// </summary>
        /// <param name="id">Category's id.</param>
        /// <returns>A <see cref="Category"/>.</returns>
        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = _categoryRepository.Get(id);
            if (category == null)
                NotFound();
            return Ok(category);           
        }
              
        /// <summary>
        /// Creates a category
        /// </summary>
        /// <param name="category">The <see cref="Category"/> to be created.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/></returns>
        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category)
        {           
            var result = await _categoryRepository.Post(category);
            return result ? (ActionResult)CreatedAtAction(nameof(Get), new { id = category.Id }, category) : BadRequest();
        }

        /// <summary>
        /// Deletes a category by its id.
        /// </summary>
        /// <param name="id">Category's id.</param>
        /// <returns>A <see cref="ActionResult"/> object.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _categoryRepository.Delete(id);
            return result ? (ActionResult)Ok() : BadRequest();
        }

        /// <summary>
        /// Updates an specific category.
        /// </summary>
        /// <param name="category">An updated <see cref="Category"/></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Category>> Put(Category category)
        {
            var result = await _categoryRepository.Put(category);
            return result ? (ActionResult)Ok() : BadRequest();
        }
    }
}
