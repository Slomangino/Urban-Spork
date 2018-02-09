using System;

namespace UrbanSpork.Common.DataTransferObjects
{
    public class UserDTO : UserInputDTO
    {
        public Guid UserID { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string Access { get; set; }
        public string Equipment { get; set; }
    }
}
