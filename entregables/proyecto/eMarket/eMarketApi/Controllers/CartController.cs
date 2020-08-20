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

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(Cart cart)
        {
            var result = await Task.Run(() => _cartRepository.CheckoutCart(cart));
            return result;
        }

        [HttpGet("{username}")]
        public ActionResult<List<Order>> Get(string username)
        {
            return Ok(_cartRepository.GetPastOrders(username));
        }
    }
}
