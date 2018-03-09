using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.User
{
    public class CreateUserInputDTO
    {
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
