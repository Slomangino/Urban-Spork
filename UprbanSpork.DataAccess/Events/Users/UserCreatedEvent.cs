
using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public UserDTO UserDTO{ get; set; }
        // public DateTime DateCreated { get; set; }

        public UserCreatedEvent(UserDTO userDTO)
        {
            UserDTO = userDTO;
            //UserDTO.userId = userId;
            //DateCreated = dateCreated;
        }
    }
}