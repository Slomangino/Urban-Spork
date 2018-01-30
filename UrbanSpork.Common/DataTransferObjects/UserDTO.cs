using System;
namespace UrbanSpork.Common.DataTransferObjects
{
    public class UserDTO
    {
        public Guid userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public bool isActive { get; set; }
        public DateTime dateCreated { get; set; }
        public UserAccessDTO userAccess { get; set; }
        public UserEquipmentDTO userEquipment { get; set; }
    }
}
