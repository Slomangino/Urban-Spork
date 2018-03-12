using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Departments
{
    public class DepartmentCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public DepartmentCreatedEvent() { }

        public DepartmentCreatedEvent(CreateDepartmentDTO dto)
        {
            Name = dto.Name;
            TimeStamp = DateTime.Now;
            IsActive = dto.IsActive;
        }
    }
}
