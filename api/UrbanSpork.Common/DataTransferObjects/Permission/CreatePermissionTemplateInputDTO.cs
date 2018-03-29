using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.Permission
{
    public class CreatePermissionTemplateInputDTO
    {
        public string Name { get; set; }
        public Dictionary<Guid, string> TemplatePermissions { get; set; }
    }
}
