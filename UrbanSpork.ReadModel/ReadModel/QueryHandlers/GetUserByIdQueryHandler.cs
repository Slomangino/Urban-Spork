﻿using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.ReadModel;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDTO>
    {
        private IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserDTO> Handle(GetUserByIdQuery query)
        {
            return _userRepository.GetSingleUser(query.Id);
        }

    }
}