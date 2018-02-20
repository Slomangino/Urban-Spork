using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserEnabledEvent : IEvent
    {
        public UserDTO UserDTO { get; set; }
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public UserEnabledEvent() { }

        public UserEnabledEvent(UserDTO dto)
        {
            Id = dto.UserID;
            this.UserDTO = dto;
        }
    }
}
