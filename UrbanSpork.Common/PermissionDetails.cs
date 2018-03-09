using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace UrbanSpork.Common
{
    public class PermissionDetails
    {
        public string EventType { get; set; }
        public bool IsPending { get; set; }
        public string Reason { get; set; }
        public DateTime RequestDate { get; set; }
        public Guid RequestedBy { get; set; }
        public Guid RequestedFor { get; set; }
        
    }
}
