using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.DataTransferObjects;

namespace UrbanSpork.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UrbanDbContext _context;
        private IMapper _mapper;

        public UserRepository(UrbanDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<UserDTO> GetSingleUser(int id)
        {
            var foo = _context.Users
                .Single(b => b.UserID == 1);

            return Task.FromResult(_mapper.Map<UserDTO>(foo));
        }

        public void CreateUser(Users message)
        {
            _context.Users.Add(message);
            _context.SaveChanges();
            //_context.FindAsync(message);
            //return Task.FromResult(message);
        }
    }
}
