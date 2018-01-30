using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess
{
    public class UserAggregate : AggregateRoot
    {
        public UserDTO userDTO { get; set; }
        public string FirstName { get; private set; } = "";
        public DateTime DateCreated { get; set; }

        private UserAggregate() { }

        protected UserAggregate(Guid userId, UserDTO userDTO, DateTime dateCreated)
        {
            this.userDTO = userDTO;
            this.userDTO.userId = userId;
            this.userDTO.dateCreated = dateCreated;
            ApplyChange(new UserCreatedEvent(userDTO));
        }

        public static UserAggregate CreateNewUser(UserDTO userDTO)
        {
            var userId = Guid.NewGuid();
            var creationDate = DateTime.Today;
            return new UserAggregate(userId, userDTO, creationDate);
        }

        private void Apply(UserCreatedEvent @event)
        {
            //userDTO = @event.UserDTO;
            //DateCreated = @event.DateCreated;
        }
    }
}
