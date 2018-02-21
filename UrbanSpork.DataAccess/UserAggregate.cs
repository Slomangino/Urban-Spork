using UrbanSpork.CQRS.Domain;
using System;
using Newtonsoft.Json;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess
{
    public class UserAggregate : AggregateRoot
    {
        public UserDTO userDTO { get; private set; }
        public DateTime DateCreated { get; private set; }

        private UserAggregate() { }

        protected UserAggregate(UserDTO dto)
        {
            Id = Guid.NewGuid();
            dto.UserID = Id;
            ApplyChange(new UserCreatedEvent(dto));
        }

        public static UserAggregate CreateNewUser(UserDTO dto)
        {
            return new UserAggregate(dto);
        }

        public void UpdateUserPersonalInfo(UserDTO dto)
        {
            ApplyChange(new UserUpdatedEvent(dto));
        }

        public void DisableSingleUser(UserDTO dto)
        {
            ApplyChange(new UserDisabledEvent(dto));
        }

        public void EnableSingleUser(UserDTO dto)
        {
            ApplyChange(new UserEnabledEvent(dto));
        }

        public void UpdateSingleUserPermissions(UserPermissionsInputDTO dto)
        {
            ApplyChange(new UserPermissionsUpdatedEvent(dto));
        }

        private void Apply(UserCreatedEvent @event)
        {
            DateCreated = @event.UserDTO.DateCreated;
            userDTO = @event.UserDTO;
            userDTO.IsActive = true;
        }

        private void Apply(UserUpdatedEvent @event)
        {
            userDTO = @event.UserDTO;
        }

        private void Apply(UserDisabledEvent @event)
        {
            userDTO.IsActive = false;
        }

        private void Apply(UserEnabledEvent @event)
        {
            userDTO.IsActive = true;
        }

        private void Apply(UserPermissionsUpdatedEvent @event)
        {
            userDTO.Access = JsonConvert.SerializeObject(@event.Dto.PermissionList);
        }
    }
}
