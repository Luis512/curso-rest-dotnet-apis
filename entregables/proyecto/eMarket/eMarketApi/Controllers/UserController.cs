using eMarketApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using eMarketDB.Infrastructure.Data;
using eMarketDomain.Models;

namespace eMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Controller that controlls the user section.
        /// </summary>
        /// <param name="IUserRepository">Dependency injection of <see cref="IUserRepository"/></param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        public ActionResult Post(Usuario user)
        {
            var userFound = _userRepository.Login(user);
            return userFound ? (ActionResult)Ok() : NotFound();
        }

        [HttpGet("{username}")]
        public ActionResult<Usuario> Get(string username)
        {
            var userFound = _userRepository.GetUserInformation(username);
            return userFound != null ? (ActionResult)Ok(userFound) : NotFound();
        }
    }
}
