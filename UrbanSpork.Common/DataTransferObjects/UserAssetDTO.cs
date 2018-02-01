using System;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.Common.DataTransferObjects
{
    public class UserAssetDTO
    {
        public UserAccessDTO userAccess { get; set; }
        public UserEquipmentDTO userEquipment { get; set; }
    }
}
