using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserDisabledEvent : IEvent
    {
        public UserDTO UserDTO { get; private set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public UserDisabledEvent() { }

        public UserDisabledEvent(UserDTO dto)
        {
            Id = dto.UserID;
            UserDTO = dto;
        }

    }
}
