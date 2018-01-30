using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.CQRS.Interfaces;
using UrbanSpork.Common.DataTransferObjects;
using AutoMapper;

namespace UrbanSpork.DataAccess
{
    public class UserManager : IUserManager
    {
        private readonly UrbanDbContext _context;

        public UserManager(UrbanDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> CreateNewUser(UserInputDTO userInputDTO)
        {
            var userDTO = Mapper.Map<UserDTO>(userInputDTO);

            Console.WriteLine("New User created!");
            var user = UserAggregate.CreateNewUser(userDTO);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Context saved {user.FirstName}");
            return user.userDTO;
        }
    }
}
