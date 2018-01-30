﻿using System;
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

        void CreateUser(Users message);
    }
}
