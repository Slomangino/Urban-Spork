using System;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.CQRS.Interfaces
{
    public interface IUserManager
    {
        Task<UserDTO> CreateNewUser(UserInputDTO userInputDTO);
        Task<UserDTO> UpdateUser(Guid id, UserInputDTO userInputDTO);
    }
}