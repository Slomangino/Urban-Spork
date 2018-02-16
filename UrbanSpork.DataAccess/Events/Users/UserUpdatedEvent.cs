using UrbanSpork.CQRS.Events;
using System;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserUpdatedEvent : IEvent
    {
        public UserDTO UserDTO { get; }
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public UserUpdatedEvent() { }

        public UserUpdatedEvent(UserDTO UserDTO)
        {
            Id = UserDTO.UserID;
            this.UserDTO = UserDTO;
        }
    }
}
