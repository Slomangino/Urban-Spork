using System;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events
{
    public class PermissionCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; }

        public PermissionCreatedEvent() { }

        public PermissionCreatedEvent(CreateNewPermissionDTO dto)
        {
            Name = dto.Name;
            Description = dto.Description;
            IsActive = dto.IsActive;
            Image = dto.Image;
        }
    }
}
