using System;

namespace UrbanSpork.Common.DataTransferObjects.User
{
    public class UserManagementDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
    }
}