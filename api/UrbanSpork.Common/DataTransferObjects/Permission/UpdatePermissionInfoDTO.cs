using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.Permission
{
    public class UpdatePermissionInfoDTO
    {
        public Guid Id { get; set; }
        public Guid UpdatedById { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
