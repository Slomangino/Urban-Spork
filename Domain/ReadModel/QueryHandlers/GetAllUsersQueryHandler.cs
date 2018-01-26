﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Npgsql;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.Domain.Interfaces.ReadModel;
using UrbanSpork.DataAccess.Repositories;

namespace UrbanSpork.Domain.ReadModel.QueryHandlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<UserDTO>>
    {
        private IUserRepository _userRepository;


        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<UserDTO>> Handle(GetAllUsersQuery query)
        {
            return _userRepository.GetAllUsers();
        }
    }
}
