using eMarketApi.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        /// <summary>
        /// Constructor thta manages the reviews section.
        /// </summary>
        /// <param name="reviewRepository"></param>
        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        /// <summary>
        /// Gets a review by its id.
        /// </summary>
        /// <param name="id">Review's id</param>
        /// <returns>A <see cref="ActionResult"/></returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var review = _reviewRepository.Get(id);
            if (review == null)
                NotFound();
            return Ok(review);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ByProd/{id}")]
        public ActionResult GetReviewByProducts(int id)
        {
            return Ok(_reviewRepository.GetReviewByProduct(id));
        }

        /// <summary>
        /// Create a review.
        /// </summary>
        /// <param name="review">A <see cref="Review"/> to be created.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/> result.</returns>
        [HttpPost]
        public async Task<ActionResult<Review>> Post(Review review)
        {
            await Task.Run(() => _reviewRepository.Post(review));
            return CreatedAtAction(nameof(Get), new { id = review.Id }, review);

        }             
    }
}
