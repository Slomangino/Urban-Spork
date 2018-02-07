using System;

namespace UrbanSpork.Common.DataTransferObjects
{
    public class UserDTO : UserInputDTO
    {
        public Guid userID { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public string access { get; set; }
        public string equipment { get; set; }
    }
}
