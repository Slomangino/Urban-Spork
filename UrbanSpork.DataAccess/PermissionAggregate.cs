using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess.Events;

namespace UrbanSpork.DataAccess
{
    public class PermissionAggregate : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime DateCreated { get; private set; }

        private PermissionAggregate() { }

        protected PermissionAggregate(CreateNewPermissionDTO dto)
        {
            Id = Guid.NewGuid();
            ApplyChange(new PermissionCreatedEvent(dto));
        }

        public static PermissionAggregate CreateNewPermission(CreateNewPermissionDTO dto)
        {
            return new PermissionAggregate(dto);
        }

        public void UpdatePermissionInfo(UpdatePermissionInfoDTO dto)
        {
            ApplyChange(new PermissionInfoUpdatedEvent(dto));
        }

        public void DisablePermission()
        {
            ApplyChange(new PermissionDiabledEvent());
        }

        public void EnablePermission()
        {
            ApplyChange(new PermissionEnabledEvent());
        }

        private void Apply(PermissionCreatedEvent @event)
        {
            Name = @event.Name;
            Description = @event.Description;
            IsActive = @event.IsActive;
            DateCreated = @event.TimeStamp;
        }

        private void Apply(PermissionInfoUpdatedEvent @event)
        {
            Name = @event.Name;
            Description = @event.Description;
        }

        private void Apply(PermissionDiabledEvent @event)
        {
            IsActive = @event.IsActive;
        }

        private void Apply(PermissionEnabledEvent @event)
        {
            IsActive = @event.IsActive;
        }
    }
}
