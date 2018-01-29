using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.Domain.Interfaces.ReadModel;

namespace UrbanSpork.Domain.ReadModel.QueryHandlers
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
