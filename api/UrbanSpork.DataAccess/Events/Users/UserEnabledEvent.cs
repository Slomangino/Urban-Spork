using System;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserEnabledEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public bool IsActive { get; set; }
        public Guid ByAgg { get; set; }

        public UserEnabledEvent() { }

        public UserEnabledEvent(Guid byAgg)
        {
            IsActive = true;
            ByAgg = byAgg;
        }
    }
}
