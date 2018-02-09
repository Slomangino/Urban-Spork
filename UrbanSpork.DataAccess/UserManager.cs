using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.CQRS.Interfaces;
using UrbanSpork.Common.DataTransferObjects;
using AutoMapper;
using UrbanSpork.DataAccess.Repositories;
using CQRSlite.Domain;

namespace UrbanSpork.DataAccess
{
    public class UserManager : IUserManager
    {
        private readonly ISession _session;
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository, ISession session)
        {
            _userRepository = userRepository;
            _session = session;
        }

        public Task<UserDTO> CreateNewUser(UserInputDTO userInputDTO)
        {
            var userDTO = Mapper.Map<UserDTO>(userInputDTO);
            var userAgg = UserAggregate.CreateNewUser(userDTO);

            _session.Add(userAgg);
            _session.Commit();

            return Task.FromResult(userAgg.userDTO);



            //var user = Mapper.Map<User>(userAgg.userDTO);
            //user.UserID = userAgg.Id;

            //var events = userAgg.GetUncommittedChanges();

            //_userRepository.SaveEvent(events);
            //_userRepository.CreateUser(user);


        }

        public async Task<UserDTO> UpdateUser(Guid id, UserInputDTO userInputDTO)
        {
            var userDTO = Mapper.Map<UserDTO>(userInputDTO);

            var userAgg = await _session.Get<UserAggregate>(id);

            //userAgg.UpdateUser(userInputDTO);

            //var user = Mapper.Map<User>(userAgg.userDTO);
            //var events = userAgg.GetUncommittedChanges();

            //_userRepository.SaveEvent(events);
            //_userRepository.UpdateUser(user);

            return userAgg.userDTO;
        }
    }
}
