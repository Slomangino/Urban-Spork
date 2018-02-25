using System;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;

namespace UrbanSpork.DataAccess
{
    public interface IUserManager
    {
        Task<UserDTO> CreateNewUser(CreateUserInputDTO input);
        Task<UserDTO> UpdateUserInfo(Guid id, UpdateUserInformationDTO input);
        Task<UserDTO> DisableSingleUser(Guid id);
        Task<UserDTO> EnableSingleUser(Guid id);
        Task<UserDTO> UpdateUserPermissions(UpdateUserPermissionsDTO input);
    }
}