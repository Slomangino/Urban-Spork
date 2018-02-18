using AutoMapper;
using UrbanSpork.CQRS.Domain;
using System;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces;
using UrbanSpork.DataAccess.Repositories;

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

        public async Task<UserDTO> DisableSingleUser(Guid id)
        {
            var userAgg = await _session.Get<UserAggregate>(id);

            if (userAgg.userDTO.IsActive) // if the user is already disabled, do not do anything
            {
                var dto = userAgg.userDTO;
                userAgg.DisableSingleUser(dto);
                await _session.Commit();
            }

            return userAgg.userDTO;
        }
    }
}
