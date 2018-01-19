using System;
namespace UrbanSpork.Domain.DataTransfer
{
    public class UserDTO
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public bool isActive { get; set; }
        public UserAssetDTO userAssets { get; set; }
    }
}
