using UrbanSpork.CQRS.Domain;
using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess
{
    public class UserAggregate : AggregateRoot
    {
        public UserDTO userDTO { get; private set; }
        public DateTime DateCreated { get; private set; }

        private UserAggregate() { }

        protected UserAggregate(UserDTO userDTO)
        {
            Id = userDTO.UserID;
            DateCreated = userDTO.DateCreated;
            ApplyChange(new UserCreatedEvent(userDTO));
        }

        public static UserAggregate CreateNewUser(UserDTO userDTO)
        {
            var dto = userDTO;
            dto.UserID = Guid.NewGuid();
            dto.DateCreated = DateTime.Today;
            return new UserAggregate(dto);
        }

        public void UpdateUserPersonalInfo(UserDTO userDTO)
        {
            ApplyChange(new UserUpdatedEvent(userDTO));
        }

        private void Apply(UserCreatedEvent @event)
        {
            DateCreated = @event.TimeStamp;
            userDTO = @event.UserDTO;
        }

        private void Apply(UserUpdatedEvent @event)
        {
            userDTO = @event.UserDTO;
        }
    }
}
