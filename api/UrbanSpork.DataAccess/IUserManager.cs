using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;

namespace UrbanSpork.DataAccess
{
    public interface IUserManager
    {
        Task<UserDTO> UserPermissionsRequested(RequestUserPermissionsDTO input);
        Task<UserDTO> DenyUserPermissionRequest(DenyUserPermissionRequestDTO input);
        Task<UserDTO> GrantUserPermission(GrantUserPermissionDTO input);
        Task<UserDTO> RevokePermissions(RevokeUserPermissionDTO input);
    }
}