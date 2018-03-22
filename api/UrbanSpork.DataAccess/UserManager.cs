using AutoMapper;
using UrbanSpork.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.DataAccess.Repositories;

namespace UrbanSpork.DataAccess
{
    public class UserManager : IUserManager
    {
        private readonly ISession _session;
        private readonly IUserRepository _userRepository;
        private readonly Email _email = new Email();
        
        public UserManager(IUserRepository userRepository, ISession session)
        {
            _session = session;
           
        }

        public async Task<UserDTO> CreateNewUser(CreateUserInputDTO input)
        {
            var userAgg = UserAggregate.CreateNewUser(input);
            await _session.Add(userAgg);
            await _session.Commit();
            var userDTO = Mapper.Map<UserDTO>(await _session.Get<UserAggregate>(userAgg.Id));


            _email.SendUserCreatedMessage(userDTO);
            return userDTO;
        }

        public async Task<UpdateUserInformationDTO> UpdateUserInfo(Guid id, UpdateUserInformationDTO dto)
        {
            var userAgg = await _session.Get<UserAggregate>(id);

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
            
            input.Requests = VerifyActions(
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
                _email.SendPermissionsRequestedMessage(forAgg, permissionAggregates);
            }
            return Mapper.Map<UserDTO>(forAgg);
        }

        public async Task<UserDTO> DenyUserPermissionRequest(DenyUserPermissionRequestDTO input)
        {
            var forAgg = await _session.Get<UserAggregate>(input.ForId);
            var byAgg = await _session.Get<UserAggregate>(input.ById);

            input.PermissionsToDeny = VerifyActions(
                forAgg,
                byAgg,
                input.PermissionsToDeny,
                new List<string>
                {
                    typeof(UserPermissionRevokedEvent).FullName,
                    typeof(UserPermissionRequestDeniedEvent).FullName,
                    typeof(UserPermissionGrantedEvent).FullName
                });
            if (input.PermissionsToDeny.Any())
            {
                var permissionAggregates = new List<PermissionAggregate>();
                foreach (var request in input.PermissionsToDeny)
                {
                    permissionAggregates.Add(await _session.Get<PermissionAggregate>(request.Key));
                }
                forAgg.DenyPermissionRequest(byAgg, permissionAggregates, input);
                await _session.Commit();
                _email.SendRequestDeniedMessage(forAgg, permissionAggregates);
            }
            return Mapper.Map<UserDTO>(forAgg);
        }

        public async Task<UserDTO> GrantUserPermission(GrantUserPermissionDTO input)
        {
            var forAgg = await _session.Get<UserAggregate>(input.ForId);
            var byAgg = await _session.Get<UserAggregate>(input.ById);

            input.PermissionsToGrant = VerifyActions(
                forAgg,
                byAgg,
                input.PermissionsToGrant,
                new List<string>
                {
                    typeof(UserPermissionGrantedEvent).FullName
                });

            if (input.PermissionsToGrant.Any())
            {
                var permissionAggregates = new List<PermissionAggregate>();
                foreach (var request in input.PermissionsToGrant)
                {
                    permissionAggregates.Add(await _session.Get<PermissionAggregate>(request.Key));
                }
                forAgg.GrantPermission(byAgg, permissionAggregates, input);
                await _session.Commit();
                _email.SendPermissionsGrantedMessage(forAgg, permissionAggregates);
            }

            return Mapper.Map<UserDTO>(await _session.Get<UserAggregate>(forAgg.Id));
        }

        public async Task<UserDTO> RevokePermissions(RevokeUserPermissionDTO input)
        {
            var forAgg = await _session.Get<UserAggregate>(input.ForId);
            var byAgg = await _session.Get<UserAggregate>(input.ById);

            input.PermissionsToRevoke = VerifyActions(
                forAgg,
                byAgg,
                input.PermissionsToRevoke,
                new List<string> {
                    typeof(UserPermissionsRequestedEvent).FullName,
                    typeof(UserPermissionRevokedEvent).FullName,
                    typeof(UserPermissionRequestDeniedEvent).FullName
                });

            if (input.PermissionsToRevoke.Any())
            {
                var permissionAggregates = new List<PermissionAggregate>();
                foreach (var request in input.PermissionsToRevoke)
                {
                    permissionAggregates.Add(await _session.Get<PermissionAggregate>(request.Key));
                }

                forAgg.RevokePermission(byAgg, input);
                await _session.Commit();
                _email.SendPermissionsRevokedMessage(forAgg, permissionAggregates);
            }

            
            return Mapper.Map<UserDTO>(forAgg);
        }

        /**
         * filters actions taken according to the forAgg's permissionList's event type, and the event types being passed in
         *
         * Ex: if a forAgg's permission list has VisualStudio permission's  eventType as "revoked", we do not want to be able to revoke it again, so we
         * remove it from the list of actions.
         */
        private Dictionary<Guid, PermissionDetails> VerifyActions(UserAggregate forAgg, UserAggregate byAgg, Dictionary<Guid, PermissionDetails> requests, List<string> eventTypesToRemove)
        {
            var result = new Dictionary<Guid, PermissionDetails>();
            var markedForRemoval = new List<Guid>();
            foreach (var request in requests)
            {
                result[request.Key] = request.Value;
                //if forAgg's permission list contains a definition for that permission
                if (forAgg.PermissionList.ContainsKey(request.Key))
                {
                    // if the definition of that permission is one of the eventTypes to remove, remove it from requests.
                    if (eventTypesToRemove.Contains(
                        JsonConvert.DeserializeObject<string>(forAgg.PermissionList[request.Key].EventType)))
                    {
                        markedForRemoval.Add(request.Key);
                    }
                    else
                    {
                        //might not even need this******because admin rights are handled in the aggregate.
                        //(admins can override eventTypes) Otherwise, if they are not an admin, remove it from requests
                        if (!byAgg.IsAdmin)
                        {
                            markedForRemoval.Add(request.Key);
                        }
                    }
                } // if the permission list does not contain, this case will be handled by the aggregate since admins can do 
                  // whatever they want. and the aggregate will restrict user actions based on that
            }

            markedForRemoval.ForEach(a => result.Remove(a));

            return result;
        }
    }
}
