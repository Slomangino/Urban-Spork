using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Department;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess.Events.Departments;

namespace UrbanSpork.DataAccess
{
    public class DepartmentAggregate : AggregateRoot
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime DateCreated { get; private set; }

        private DepartmentAggregate() { }

        protected DepartmentAggregate(CreateDepartmentDTO dto)
        {
            Id = Guid.NewGuid();
            ApplyChange(new DepartmentCreatedEvent(dto));
        }

        public static DepartmentAggregate CreateNewDepartment(CreateDepartmentDTO dto)
        {
            return new DepartmentAggregate(dto);
        }

        public void UpdateDepartment(UpdateDepartmentDTO dto)
        {
            ApplyChange(new DepartmentUpdatedEvent(dto));
        }

        public void DisableDepartment()
        {
            ApplyChange(new DepartmentDisabledEvent());
        }

        public void EnableDepartment()
        {
            ApplyChange(new DepartmentEnabledEvent());
        }

        private void Apply(DepartmentCreatedEvent @event)
        {
            Name = @event.Name;
            IsActive = @event.IsActive;
            DateCreated = @event.TimeStamp;
        }

        private void Apply(DepartmentUpdatedEvent @event)
        {
            Name = @event.Name;
        }

        private void Apply(DepartmentDisabledEvent @event)
        {
            IsActive = @event.IsActive;
        }

        private void Apply(DepartmentEnabledEvent @event)
        {
            IsActive = @event.IsActive;
        }
    }
}
