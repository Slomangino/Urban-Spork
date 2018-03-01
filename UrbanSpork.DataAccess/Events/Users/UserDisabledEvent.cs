using System;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserDisabledEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public bool IsActive { get; set; }

        public UserDisabledEvent()
        {
            IsActive = false;
        }

    }
}
