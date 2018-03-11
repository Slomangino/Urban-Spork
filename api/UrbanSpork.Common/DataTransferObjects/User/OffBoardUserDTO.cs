using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.User
{
    public class OffBoardUserDTO
    {
        
        public Dictionary<Guid, DetailedUserPermissionInfo> PermissionList { get; set; }
    }
}
