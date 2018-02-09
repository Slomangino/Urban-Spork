using CQRSLite.Queries.Query;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetAllUsersQuery : IQuery<List<UserDTO>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
