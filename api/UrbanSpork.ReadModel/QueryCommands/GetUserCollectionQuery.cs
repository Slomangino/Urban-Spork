using System.Collections.Generic;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetUserCollectionQuery : IQuery<List<UserDetailProjectionDTO>>
    {
        public UserFilterCriteria FilterCriteria; 
    }
}
