using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.CQRS.Interfaces;
using UrbanSpork.Common.DataTransferObjects;
using AutoMapper;
using UrbanSpork.CQRS.Interfaces.Events;

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
            
            var userAgg = UserAggregate.CreateNewUser(userDTO);

            var user = Mapper.Map<User>(userAgg.userDTO);
            var events = userAgg.GetUncommittedChanges();

            //Add to event store
            //foreach (IEvent e in events)
            //{
            //    await _context.UserEvents.AddAsync((UserEvents)e);
            //}

            //update user table
            await _context.Users2.AddAsync(user);

            //commit
            await _context.SaveChangesAsync();


            return userAgg.userDTO;
        }
    }
}
