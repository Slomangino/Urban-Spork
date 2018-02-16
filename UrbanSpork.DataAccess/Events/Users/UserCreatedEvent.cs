
using UrbanSpork.CQRS.Events;
using System;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserCreatedEvent : IEvent
    {
        public UserDTO UserDTO{ get; private set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public UserCreatedEvent() { }

        public UserCreatedEvent(UserDTO userDTO)
        {
            Id = userDTO.UserID;
            UserDTO = userDTO;
        }
    }
}