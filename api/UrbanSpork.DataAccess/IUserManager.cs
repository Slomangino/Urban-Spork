using System;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;

namespace UrbanSpork.DataAccess
{
    public interface IUserManager
    {
        Task<UserDTO> CreateNewUser(CreateUserInputDTO input);
        Task<UpdateUserInformationDTO> UpdateUserInfo(Guid id, UpdateUserInformationDTO input);
        Task<UserDTO> DisableSingleUser(DisableUserInputDTO input);
        Task<UserDTO> EnableSingleUser(EnableUserInputDTO input);
        Task<UserDTO> UserPermissionsRequested(RequestUserPermissionsDTO input);
        Task<UserDTO> DenyUserPermissionRequest(DenyUserPermissionRequestDTO input);
        Task<UserDTO> GrantUserPermission(GrantUserPermissionDTO input);
        Task<UserDTO> RevokePermissions(RevokeUserPermissionDTO input);
    }
}