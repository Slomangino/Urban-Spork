using UrbanSpork.CQRS.Domain;
using System;
using System.Collections.Generic;
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

        public void DisableSingleUser()
        {
            //business Logic here!
            ApplyChange(new UserDisabledEvent());
        }

        public void EnableSingleUser()
        {
            //business Logic here!
            ApplyChange(new UserEnabledEvent());
        }

        public void UserRequestedPermissions(UpdateUserPermissionsDTO dto)
        {
            //business Logic here!
            // check to see if ById is an admin?
            //check to see if permission is active
            ApplyChange(new UserPermissionsRequestedEvent(dto));
        }

        public void DenyPermissionRequest(DenyUserPermissionRequestDTO dto)
        {
            //business Logic here!
            ApplyChange(new UserPermissionRequestDeniedEvent(dto));
        }

        public void GrantPermission(GrantUserPermissionDTO dto)
        {
            //business Logic here!
            ApplyChange(new UserPermissionGrantedEvent(dto));
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
                PermissionList[request.Key] = request.Value; //add if not there, update if it is. Losing the DateCreated for some reason.
            }
        }

        private void Apply(UserPermissionRequestDeniedEvent @event)
        {
            PermissionList[@event.PermissionId] = new PermissionDetails
            {
                EventType = @event.GetType().FullName,
                IsPending = false,
                Reason = @event.ReasonForDenial,
                RequestDate = @event.TimeStamp,
                RequestedBy = @event.ById,
                RequestedFor = @event.ForId
            };
        }

        private void Apply(UserPermissionGrantedEvent @event)
        {
            foreach (var request in @event.PermissionsToGrant)
            {
                PermissionList[request.Key] = request.Value; 
            }
        }
    }
}
