using System;

namespace UrbanSpork.Common.DataTransferObjects
{
    public class UserDTO 
    {
        public Guid userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public bool IsActive { get; set; }
        public DateTime InitialApprovedDate { get; set; }
        public string access { get; set; }
        public string equipment { get; set; }
    }
}
