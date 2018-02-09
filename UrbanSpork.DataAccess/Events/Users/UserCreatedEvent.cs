
using CQRSlite.Events;
using System;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserCreatedEvent : IEvent
    {
        public UserDTO UserDTO{ get; set; }
        public Guid Id { get; set; }
        public int Version { get; set; } = 0;
        public DateTimeOffset TimeStamp { get; set; } = DateTime.Now;

        public UserCreatedEvent() { }

        public UserCreatedEvent(UserDTO userDTO)
        {
            Id = userDTO.UserID;
            UserDTO = userDTO;
        }
    }
}