using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces;

namespace UrbanSpork.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UrbanDbContext _context;
        private IUserManager _userManager;

        public UserRepository(UrbanDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task<UserDTO> GetSingleUser(int id)
        {
            var foo = _context.Users2
                .Single(b => b.UserID == id);

            return Task.FromResult(Mapper.Map<UserDTO>(foo));
        }

        public Task<List<UserDTO>> GetAllUsers()
        {
            var users = _context.Users2.ToList();
            var userList = new List<UserDTO>();
            foreach(var user in users)
            {
                var userDto = Mapper.Map<UserDTO>(user);
                userList.Add(userDto);
            }
            return Task.FromResult(userList);
        }

        public void CreateUser(User message)
        {
            //_userManager.CreateNewUser();
            _context.Users2.Add(message);
            _context.SaveChanges();
            //_context.FindAsync(message);
            //return Task.FromResult(message);
        }
    }
}
