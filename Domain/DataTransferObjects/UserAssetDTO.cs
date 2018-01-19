using System;
namespace UrbanSpork.Domain.DataTransferObjects
{
    public class UserAssetDTO
    {
        public UserAccessDTO userAccess { get; set; }
        public UserEquipmentDTO userEquipment { get; set; }
    }
}
