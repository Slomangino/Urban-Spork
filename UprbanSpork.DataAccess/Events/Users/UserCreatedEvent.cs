
using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.Events;
using UrbanSpork.DataAccess.DataAccess;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserCreatedEvent : IEvent
    {
        public UserDTO UserDTO{ get; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Version { get; set; } = 0;
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public UserCreatedEvent(UserDTO userDTO)
        {
            UserDTO = userDTO;
        }
    }
}