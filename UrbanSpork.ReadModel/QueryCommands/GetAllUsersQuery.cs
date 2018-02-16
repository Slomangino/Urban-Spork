using System.Collections.Generic;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetAllUsersQuery : IQuery<List<UserDTO>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
