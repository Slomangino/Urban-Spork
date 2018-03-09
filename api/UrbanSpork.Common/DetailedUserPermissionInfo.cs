using System;
using System.Threading;

namespace UrbanSpork.Common
{
    public class DetailedUserPermissionInfo
    {
        private string ApproverFullName { get; set; }
        public string ApproverFirstName { get; set; }
        public string ApproverLastName { get; set; }
        public string PermissionStatus { get; set; }
        public string PermissionName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}