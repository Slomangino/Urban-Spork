using AutoMapper;
using UrbanSpork.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.DataAccess.Events.Users;

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
            var userDTO = Mapper.Map<UserDTO>(await _session.Get<UserAggregate>(userAgg.Id));

            return userDTO;
        }

        public async Task<UpdateUserInformationDTO> UpdateUserInfo(Guid id, UpdateUserInformationDTO dto)
        {
            var userAgg = await _session.Get<UserAggregate>(id);

            //dto.UserID = id;
            //dto.DateCreated = userAgg.DateCreated;
            //dto.IsActive = userAgg.userDTO.IsActive;

            userAgg.UpdateUserInfo(dto);

            await _session.Commit();

            var result = Mapper.Map<UpdateUserInformationDTO>(userAgg);

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

        public async Task<UserDTO> UserPermissionsRequested(RequestUserPermissionsDTO input)
        {
            var forAgg = await _session.Get<UserAggregate>(input.ForId);
            var byAgg = await _session.Get<UserAggregate>(input.ById);
            
            input.Requests = VerifyRequests(
                forAgg, 
                byAgg, 
                input.Requests,
                new List<string>{typeof(UserPermissionsRequestedEvent).FullName, typeof(UserPermissionGrantedEvent).FullName});
            
            if (input.Requests.Any())
            {
                var permissionAggregates = new List<PermissionAggregate>();
                foreach (var request in input.Requests)
                {
                    permissionAggregates.Add(await _session.Get<PermissionAggregate>(request.Key));
                }

                forAgg.UserRequestedPermissions(permissionAggregates, input);
                await _session.Commit();
            }
            return Mapper.Map<UserDTO>(forAgg);
        }

        public async Task<UserDTO> DenyUserPermissionRequest(DenyUserPermissionRequestDTO input)
        {
            var userAgg = await _session.Get<UserAggregate>(input.ForId);
            userAgg.DenyPermissionRequest(input);
            await _session.Commit();
            return Mapper.Map<UserDTO>(userAgg);
        }

        public async Task<UserDTO> GrantUserPermission(GrantUserPermissionDTO input)
        {
            var userAgg = await _session.Get<UserAggregate>(input.ForId);
            userAgg.GrantPermission(input);
            await _session.Commit();
            return Mapper.Map<UserDTO>(userAgg);
        }

        public async Task<UserDTO> RevokePermissions(RevokeUserPermissionDTO input)
        {
            var userAgg = await _session.Get<UserAggregate>(input.ForId);
            userAgg.RevokePermission(input);
            await _session.Commit();
            return Mapper.Map<UserDTO>(userAgg);
        }

        private Dictionary<Guid, PermissionDetails> VerifyRequests(UserAggregate forAgg, UserAggregate byAgg, Dictionary<Guid, PermissionDetails> requests, List<string> eventTypesToRemove)
        {
            var result = new Dictionary<Guid, PermissionDetails>();
            var markedForRemoval = new List<Guid>();
            foreach (var request in requests)
            {
                result[request.Key] = request.Value;
                if (forAgg.PermissionList.ContainsKey(request.Key))
                {
                    if (eventTypesToRemove.Contains(
                        JsonConvert.DeserializeObject<string>(forAgg.PermissionList[request.Key].EventType)))
                    {
                        markedForRemoval.Add(request.Key);
                    }
                    else
                    {
                        if (!byAgg.IsAdmin)
                        {
                            markedForRemoval.Add(request.Key);
                        }
                    }
                }
            }

            markedForRemoval.ForEach(a => result.Remove(a));

            return result;
        }
    }
}
