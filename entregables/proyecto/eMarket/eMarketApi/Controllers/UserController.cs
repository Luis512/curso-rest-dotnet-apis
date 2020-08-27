using eMarketApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using eMarketDomain.Models;

namespace eMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor of <see cref="UserController"/>
        /// </summary>
        /// <param name="IUserRepository">Dependency injection of <see cref="IUserRepository"/></param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">A <see cref="Usuario"/> object.</param>
        /// <returns>True if the user was created succesfully, otherwise returns false.</returns>
        [HttpPost]
        public ActionResult Post(Usuario user)
        {
            var userFound = _userRepository.Login(user);
            return userFound ? (ActionResult)Ok() : NotFound();
        }

        /// <summary>
        /// Gets an specific user.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <returns>A <see cref="Usuario"/> object.</returns>
        [HttpGet("{username}")]
        public ActionResult<Usuario> Get(string username)
        {
            var userFound = _userRepository.GetUserInformation(username);
            return userFound != null ? (ActionResult)Ok(userFound) : NotFound();
        }
    }
}
