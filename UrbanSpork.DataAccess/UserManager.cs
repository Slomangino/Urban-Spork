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
using UrbanSpork.DataAccess.Events.Users;
using UrbanSpork.DataAccess.Repositories;

namespace UrbanSpork.DataAccess
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;


        public UserManager(IUserRepository userRepository)
        {
            
            _userRepository = userRepository;
        }

        public Task<UserDTO> CreateNewUser(UserInputDTO userInputDTO)
        {
            var userDTO = Mapper.Map<UserDTO>(userInputDTO);
            
            var userAgg = UserAggregate.CreateNewUser(userDTO);

            var user = Mapper.Map<User>(userAgg.userDTO);
            var events = userAgg.GetUncommittedChanges();

            _userRepository.SaveEvent(events);
            _userRepository.CreateUser(user);

        
            return Task.FromResult(userAgg.userDTO);
        }

       
    }
}
