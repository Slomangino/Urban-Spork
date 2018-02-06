using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.Domain;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess
{
    public class UserAggregate : AggregateRoot
    {
        public UserDTO userDTO { get; set; }
        public DateTime DateCreated { get; set; }

        private UserAggregate() { }

        protected UserAggregate(UserDTO userDTO)
        {
            Id = userDTO.userID;
            DateCreated = userDTO.InitialApprovedDate;
            ApplyChange(new UserCreatedEvent(userDTO));
        }

        public static UserAggregate CreateNewUser(UserDTO userDTO)
        {
            var dto = userDTO;
            dto.userID = Guid.NewGuid();
            dto.InitialApprovedDate = DateTime.Today;
            return new UserAggregate(dto);
        }

        public static UserAggregate UpdateUser(UserDTO user)
        {
            var dto = user;
            return new UserAggregate(dto);
        }

        private void Apply(UserCreatedEvent @event)
        {
            userDTO = @event.UserDTO;
        }
    }
}
