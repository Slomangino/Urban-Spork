using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.Domain;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events.Users;
using UrbanSpork.Domain.Interfaces.Domain;

namespace UrbanSpork.DataAccess
{
    public class UserAggregate : AggregateRoot
    {
        public UserDTO userDTO { get; set; }
        public DateTime DateCreated { get; set; }

        private UserAggregate() { }

        protected UserAggregate(UserDTO userDTO)
        {
            Id = userDTO.userId;
            DateCreated = userDTO.dateCreated;
            ApplyChange(new UserCreatedEvent(userDTO));
        }

        public static UserAggregate CreateNewUser(UserDTO userDTO)
        {
            var dto = userDTO;
            dto.userId = Guid.NewGuid();
            dto.dateCreated = DateTime.Today;
            return new UserAggregate(dto);
        }

        private void Apply(UserCreatedEvent @event)
        {
            userDTO = @event.UserDTO;
        }
    }
}
