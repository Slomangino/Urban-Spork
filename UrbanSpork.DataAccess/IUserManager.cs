using System;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.DataAccess
{
    public interface IUserManager
    {
        Task<UserDTO> CreateNewUser(UserInputDTO userInputDTO);
        Task<UserDTO> UpdateUser(Guid id, UserInputDTO userInputDTO);
        Task<UserDTO> DisableSingleUser(Guid id);
        Task<UserDTO> EnableSingleUser(Guid id);
    }
}