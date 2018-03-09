using System;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserUpdatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public bool IsAdmin { get; set; }

        public UserUpdatedEvent() { }

        public UserUpdatedEvent(UpdateUserInformationDTO dto)
        {
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Email = dto.Email;
            Position = dto.Position;
            Department = dto.Department;
            IsAdmin = dto.IsAdmin;
        }
    }
}
