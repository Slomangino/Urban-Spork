using System;
using UrbanSpork.DataAccess.DataTransferObjects;

namespace UrbanSpork.DataAccess.DataTransferObjects
{
    public class UserAssetDTO
    {
        public UserAccessDTO userAccess { get; set; }
        public UserEquipmentDTO userEquipment { get; set; }
    }
}
