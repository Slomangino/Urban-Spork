using System;
namespace UrbanSpork.Domain.DataTransfer
{
    public class UserAssetDTO
    {
        public UserAccessDTO userAccess { get; set; }
        public UserEquipmentDTO userEquipment { get; set; }
    }
}
