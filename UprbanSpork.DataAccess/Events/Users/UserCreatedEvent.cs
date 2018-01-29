
using System;
using UrbanSpork.Domain.Interfaces.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserCreatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public string FirstName { get; set; }
        public DateTime DateCreated { get; set; }

        public UserCreatedEvent(string name, DateTime dateCreated)
        {
            FirstName = name;
            DateCreated = dateCreated;
        }
    }
}