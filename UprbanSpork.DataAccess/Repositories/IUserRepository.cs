using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSpork.DataAccess.Repositories
{
    interface IUserRepository
    {
        Task<T> GetSingleUser<T>(T message);


        // Task<T> CreateUser<T>(T message);
    }
}
