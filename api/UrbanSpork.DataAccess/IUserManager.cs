using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;

namespace UrbanSpork.DataAccess
{
    public interface IUserManager
    {
        Task<UserDTO> RevokePermissions(RevokeUserPermissionDTO input);
    }
}