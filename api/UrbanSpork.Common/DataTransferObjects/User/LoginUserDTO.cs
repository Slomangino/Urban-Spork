using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.User
{
    public class LoginUserDTO
    {
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
        public String FullName { get; set; }
    }
}
