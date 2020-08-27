using System.Collections.Generic;
using System.Threading.Tasks;
using eMarketApi.Repositories;
using eMarketDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace eMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        /// <summary>
        /// Constructor of <see cref="CartController"/>
        /// </summary>
        /// <param name="cartRepository">Dependecy injection of <see cref="ICartRepository"/></param>
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        /// <summary>
        /// Create an entry for shopping cart.
        /// </summary>
        /// <param name="cart">A <see cref="Cart"/> object to be created.</param>
        /// <returns>True if the cart was created, otherwise returns false.</returns>
        [HttpPost]
        public async Task<ActionResult<bool>> Post(Cart cart)
        {
            var result = await Task.Run(() => _cartRepository.CheckoutCart(cart));
            return result;
        }

        /// <summary>
        /// Gets the past orders of an specific user.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Order"/>.</returns>
        [HttpGet("{username}")]
        public ActionResult<List<Order>> Get(string username)
        {
            return Ok(_cartRepository.GetPastOrders(username));
        }
    }
}
