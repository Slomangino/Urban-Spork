using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;

namespace UrbanSpork.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UrbanDbContext _context;

        public UserRepository(UrbanDbContext context)
        {
            _context = context;
        }

        public Task<T> GetSingleUser<T>(T message)
        {
            throw new NotImplementedException();
        }
    }
}
