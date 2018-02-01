using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.Common.Repositories
{
    public interface IUserRepository
    {
        //Task<TResult> GetSingleUser(TResult message);

        Task<UserDTO> GetSingleUser(int id);

        Task<List<UserDTO>> GetAllUsers();

        void CreateUser(User message);
    }
}
