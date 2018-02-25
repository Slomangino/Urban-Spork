using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess.DataAccess;

namespace UrbanSpork.DataAccess.Repositories
{
    public interface IUserRepository
    {
        //Task<TResult> GetSingleUser(TResult message);

        Task<UserDTO> GetSingleUser(Guid id);

        Task<List<UserDTO>> GetAllUsers();

        //void CreateUser(User message);
        //void UpdateUserInfo(User user);
        //void SaveEvent(IEvent[] events);
    }
}
