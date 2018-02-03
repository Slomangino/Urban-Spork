using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.ReadModel;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.ReadModel.QueryHandlers
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
