using UrbanSpork.CQRS.Domain;
using System;
using System.Collections.Generic;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects;
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
        public Dictionary<Guid, PermissionRequest> PermissionList { get; private set; } = new Dictionary<Guid, PermissionRequest>();

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
            ApplyChange(new UserUpdatedEvent(dto));
        }

        public void DisableSingleUser()
        {
            ApplyChange(new UserDisabledEvent());
        }

        public void EnableSingleUser()
        {
            ApplyChange(new UserEnabledEvent());
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
    }
}
