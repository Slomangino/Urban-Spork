using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.ReadModel;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetAllUsersQuery : IQuery<List<UserDTO>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
