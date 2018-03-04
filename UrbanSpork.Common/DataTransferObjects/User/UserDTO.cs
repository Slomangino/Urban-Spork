using System;
using System.Collections.Generic;

namespace UrbanSpork.Common.DataTransferObjects
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<Guid, PermissionDetails> PermissionList { get; set; }
    }
}
