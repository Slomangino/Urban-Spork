using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.Permission
{
    public class DisablePermissionDTO
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
