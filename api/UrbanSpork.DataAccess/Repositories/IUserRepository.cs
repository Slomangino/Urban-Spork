using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO> GetSingleUser(Guid id);
        Task<List<UserDTO>> GetAllUsers();
    }
}
