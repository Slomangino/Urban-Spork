using AutoMapper;
using UrbanSpork.CQRS.Domain;
using System;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.Common.DataTransferObjects.Permission;

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

        public async Task<UserDTO> CreateNewUser(CreateUserInputDTO input)
        {
            var userAgg = UserAggregate.CreateNewUser(input);
            await _session.Add(userAgg);
            await _session.Commit();
            var userDTO = Mapper.Map<UserDTO>(userAgg);

            return userDTO;
        }

        public async Task<UserDTO> UpdateUserInfo(Guid id, UpdateUserInformationDTO dto)
        {
            var userAgg = await _session.Get<UserAggregate>(id);

            //dto.UserID = id;
            //dto.DateCreated = userAgg.DateCreated;
            //dto.IsActive = userAgg.userDTO.IsActive;

            userAgg.UpdateUserInfo(dto);

            await _session.Commit();

            var result = Mapper.Map<UserDTO>(userAgg);

            return result;
        }

        public async Task<UserDTO> DisableSingleUser(Guid id)
        {
            var userAgg = await _session.Get<UserAggregate>(id);

            if (userAgg.IsActive) 
            {
                userAgg.DisableSingleUser();
                await _session.Commit();
            }
            var result = Mapper.Map<UserDTO>(userAgg);
            return result;
        }

        public async Task<UserDTO> EnableSingleUser(Guid id)
        {
            var userAgg = await _session.Get<UserAggregate>(id);

            if (!userAgg.IsActive)
            {
                userAgg.EnableSingleUser();
                await _session.Commit();
            }
            var result = Mapper.Map<UserDTO>(userAgg);
            return result;
        }

        public async Task<UserDTO> UpdateUserPermissions(UpdateUserPermissionsDTO input)
        {
            var userAgg = await _session.Get<UserAggregate>(input.ForId);
            userAgg.UpdatePermissions(input);
            await _session.Commit();
            return Mapper.Map<UserDTO>(userAgg);
        }
    }
}
