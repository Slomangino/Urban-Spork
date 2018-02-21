using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserPermissionsUpdatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public UserPermissionsInputDTO Dto { get; set; }

        public UserPermissionsUpdatedEvent() { }

        public UserPermissionsUpdatedEvent(UserPermissionsInputDTO dto)
        {
            Id = dto.Id;
            Dto = dto;
        }
    }
}
