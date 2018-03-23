using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.Permission
{
    public class PermissionTemplateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Dictionary<Guid, string> TemplatePermissions { get; set; }
    }
}
