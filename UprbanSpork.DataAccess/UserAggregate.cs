using System;
using UrbanSpork.DataAccess.Events.Users;
using UrbanSpork.Domain.Interfaces.Domain;

namespace UrbanSpork.DataAccess
{
    public class UserAggregate : AggregateRoot
    {
        public string FirstName { get; private set; } = "";
        public DateTime DateCreated { get; set; }

        private UserAggregate() { }

        protected UserAggregate(Guid userId, DateTime dateCreated)
        {
            Id = userId;
            ApplyChange(new UserCreatedEvent("", dateCreated));
        }

        public static UserAggregate CreateNewUser()
        {
            var userId = Guid.NewGuid();
            var creationDate = DateTime.Today;
            return new UserAggregate(userId, creationDate);
        }

        private void Apply(UserCreatedEvent @event)
        {
            FirstName = @event.FirstName;
            DateCreated = @event.DateCreated;
        }
    }
}
