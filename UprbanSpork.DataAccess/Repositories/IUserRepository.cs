using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.DataTransferObjects;

namespace UrbanSpork.DataAccess.Repositories
{
    public interface IUserRepository
    {
        //Task<TResult> GetSingleUser(TResult message);

        Task<UserDTO> GetSingleUser(int id);

        void CreateUser(Users message);
    }
}
