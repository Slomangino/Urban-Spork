
using UrbanSpork.CQRS.Events;
using System;
using UrbanSpork.Common.DataTransferObjects;
using System.Collections.Generic;
using UrbanSpork.Common;

namespace UrbanSpork.DataAccess.Events.Users
{
    public class UserCreatedEvent : IEvent
    {
        
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime TimeStamp { get; set; }

        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Email { get;  set; }
        public string Position { get;  set; }
        public string Department { get;  set; }
        public bool IsAdmin { get;  set; }
        public bool IsActive { get; set; }
        public Dictionary<Guid, PermissionRequest> PermissionList { get;  set; }


        public UserCreatedEvent() { }

        public UserCreatedEvent(CreateUserInputDTO dto)
        {
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Email = dto.Email;
            Position = dto.Position;
            Department = dto.Department;
            IsAdmin = dto.IsActive;
            IsActive = dto.IsActive;
            PermissionList = dto.PermissionList;
        }
    }
}