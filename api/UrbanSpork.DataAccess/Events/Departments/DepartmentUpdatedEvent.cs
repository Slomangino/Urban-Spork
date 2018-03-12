using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Departments
{
    class DepartmentUpdatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Name { get; set; }
        public Guid UpdatedById { get; set; }

        public DepartmentUpdatedEvent() { }

        public DepartmentUpdatedEvent(UpdateDepartmentDTO dto)
        {
            Name = dto.Name;
            UpdatedById = dto.UpdatedById;
        }
    }
}
