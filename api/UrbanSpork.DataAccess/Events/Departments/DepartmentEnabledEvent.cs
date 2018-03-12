using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Departments
{
    public class DepartmentEnabledEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsActive { get; set; }


        public DepartmentEnabledEvent()
        {
            IsActive = true;
        }
    }
}
