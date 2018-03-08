using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.DataTransferObjects.Projection
{
    public class SystemActivityDTO
    {
        public string ForFullName { get; set; }
        public string ByFullName { get; set; }
        public string PermissionName { get; set; }
        public string EventType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
