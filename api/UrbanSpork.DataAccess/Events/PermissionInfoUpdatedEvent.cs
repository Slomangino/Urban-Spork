using System;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events
{
    public class PermissionInfoUpdatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UpdatedById { get; set; }

        public PermissionInfoUpdatedEvent() { }

        public PermissionInfoUpdatedEvent(UpdatePermissionInfoDTO dto)
        {
            Name = dto.Name;
            Description = dto.Description;
            UpdatedById = dto.UpdatedById;
        }  
    }
}
