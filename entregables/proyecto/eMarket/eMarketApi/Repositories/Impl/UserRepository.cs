using eMarketDB.Infrastructure.Data;
using eMarketDomain.Models;
using System;
using System.Linq;
using AutoMapper;

namespace eMarketApi.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly eMarketDBContext _context;
        private readonly IMapper _mapper;

        public UserRepository(eMarketDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool Login(Usuario user)
        {
            return _context.User
                .Where(u => u.Username.Equals(user.Username) && u.Password.Equals(user.Password))
                .Any();
        }

        public Usuario GetUserInformation(string username)
        {
            return _mapper.Map<Usuario>(_context.User
               .Where(u => u.Username.Equals(username))
               .FirstOrDefault());
        }
    }
}
