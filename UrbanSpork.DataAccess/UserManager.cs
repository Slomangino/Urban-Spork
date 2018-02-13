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
            userDTO.DateCreated = userAgg.DateCreated; //date in db was not getting set correctly
            _session.Add(userAgg);
            _session.Commit();

            return Task.FromResult(userAgg.userDTO);
        }

        public async Task<UserDTO> UpdateUser(Guid id, UserInputDTO userInputDTO)
        {
            var userAgg = await _session.Get<UserAggregate>(id);
            var userDTO = Mapper.Map<UserDTO>(userInputDTO);
            userDTO.UserID = id;
            userDTO.DateCreated = userAgg.DateCreated;

            userAgg.UpdateUserPersonalInfo(userDTO);

            await _session.Commit();

            return userAgg.userDTO;
        }
    }
}
