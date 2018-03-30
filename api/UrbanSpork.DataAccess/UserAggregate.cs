using UrbanSpork.CQRS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess
{
    public class UserAggregate : AggregateRoot
    {
        public DateTime DateCreated { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Position { get; private set; }
        public string Department { get; private set; }
        public bool IsAdmin { get; private set; }
        public bool IsActive { get; private set; }
        public Dictionary<Guid, PermissionDetails> PermissionList { get; private set; } = new Dictionary<Guid, PermissionDetails>();

        private UserAggregate() { }

        protected UserAggregate(CreateUserInputDTO dto)
        {
            Id = Guid.NewGuid();
            ApplyChange(new UserCreatedEvent(dto));
        }

        public static UserAggregate CreateNewUser(CreateUserInputDTO dto)
        {
            return new UserAggregate(dto);
        }

        public void UpdateUserInfo(UpdateUserInformationDTO dto)
        {
            //business Logic here!
            ApplyChange(new UserUpdatedEvent(dto));
        }

        public void DisableSingleUser(UserAggregate byAgg)
        {
            //business Logic here!
            if (byAgg.IsAdmin)
            {
                ApplyChange(new UserDisabledEvent(byAgg.Id));
            }
        }

        public void EnableSingleUser(UserAggregate byAgg)
        {
            //business Logic here!
            if (byAgg.IsAdmin)
            {
                ApplyChange(new UserEnabledEvent(byAgg.Id));
            }
        }

        public void UserRequestedPermissions(List<PermissionAggregate> permissions, RequestUserPermissionsDTO dto)
        {
            //business Logic here!

            //check to see if permission is active
            foreach (var permission in permissions)
            {
                if (!permission.IsActive) dto.Requests.Remove(permission.Id);
                //might need to add here some way to signal the UI to show that there was an inactive permission in their request.
                //for now, let us just remove it from the list of requests.
            }

            if (dto.Requests.Any())
            {
                ApplyChange(new UserPermissionsRequestedEvent(dto));
            }
        }

        public void DenyPermissionRequest(UserAggregate byAgg, List<PermissionAggregate> permissions, DenyUserPermissionRequestDTO dto)
        {
            //business Logic here!
            if (byAgg.IsAdmin) // cant deny permissions if the byAgg is not an admin
            {
                ApplyChange(new UserPermissionRequestDeniedEvent(dto));
            }
        }

        public void GrantPermission(UserAggregate byAgg, List<PermissionAggregate> permissions, GrantUserPermissionDTO dto)
        {
            //business Logic here!
            if (byAgg.IsAdmin)
            {
                foreach (var permission in permissions)
                {
                    if (!permission.IsActive) dto.PermissionsToGrant.Remove(permission.Id);
                }

                if (dto.PermissionsToGrant.Any())
                {
                    ApplyChange(new UserPermissionGrantedEvent(dto));
                }
            }
        }

        public void RevokePermission(UserAggregate byAgg, RevokeUserPermissionDTO dto)
        {
            // only go through with revoke if the byAgg is an admin, or if the user is operating on themselves
            if (!byAgg.IsAdmin)
            {
                if (this.Id != byAgg.Id)
                {
                    return; // invalid operation
                }
            }

            var markedForRemoval = new List<Guid>();
            foreach (var permission in dto.PermissionsToRevoke)
            {
                // do not revoke permissions that are not in the Granted state
                if (!string.Equals(JsonConvert.DeserializeObject<string>(this.PermissionList[permission.Key].EventType), 
                    typeof(UserPermissionGrantedEvent).FullName))
                {
                    markedForRemoval.Add(permission.Key);
                }
            }

            markedForRemoval.ForEach(a => dto.PermissionsToRevoke.Remove(a));

            if (dto.PermissionsToRevoke.Any())
            {
                ApplyChange(new UserPermissionRevokedEvent(dto));
            }
        }

        private void Apply(UserCreatedEvent @event)
        {
            DateCreated = @event.TimeStamp;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            Email = @event.Email;
            Position = @event.Position;
            Department = @event.Department;
            IsAdmin = @event.IsAdmin;
            IsActive = @event.IsActive;
            PermissionList = @event.PermissionList;
        }

        private void Apply(UserUpdatedEvent @event)
        {
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            Email = @event.Email;
            Position = @event.Position;
            Department = @event.Department;
            IsAdmin = @event.IsAdmin;
        }

        private void Apply(UserDisabledEvent @event)
        {
            IsActive = @event.IsActive;
        }

        private void Apply(UserEnabledEvent @event)
        {
            IsActive = @event.IsActive;
        }

        private void Apply(UserPermissionsRequestedEvent @event)
        {
            foreach (var request in @event.Requests)
            {
                PermissionList[request.Key] = request.Value; 
            }
        }

        private void Apply(UserPermissionRequestDeniedEvent @event)
        {
            foreach (var permission in @event.PermissionsToDeny)
            {
                PermissionList[permission.Key] = permission.Value;
            }
        }

        private void Apply(UserPermissionGrantedEvent @event)
        {
            foreach (var permission in @event.PermissionsToGrant)
            {
                PermissionList[permission.Key] = permission.Value; 
            }
        }

        private void Apply(UserPermissionRevokedEvent @event)
        {
            foreach (var permission in @event.PermissionsToRevoke)
            {
                PermissionList[permission.Key] = permission.Value;
            }
        }
    }
}
