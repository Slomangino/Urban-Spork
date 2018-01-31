
using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.Events;
using UrbanSpork.DataAccess.DataAccess;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserCreatedEvent : UserEvents, IEvent
    {
        public UserDTO UserDTO{ get; }

        public UserCreatedEvent(UserDTO userDTO)
        {
            UserDTO = userDTO;
        }
    }
}