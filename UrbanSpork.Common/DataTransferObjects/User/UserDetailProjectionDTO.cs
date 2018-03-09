using System;
using System.Collections.Generic;
using UrbanSpork.Common;

namespace UrbanSpork.Common.DataTransferObjects.User
{
    public class UserDetailProjectionDTO
    {
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public Dictionary<Guid, DetailedUserPermissionInfo> PermissionList { get; set; }
    }
}