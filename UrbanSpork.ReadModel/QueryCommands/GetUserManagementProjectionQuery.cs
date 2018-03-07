using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetUserManagementProjectionQuery : IQuery<List<UserManagementDTO>>
    {
        public UserFilterCriteria FilterCriteria { get; private set; }

        public GetUserManagementProjectionQuery(UserFilterCriteria filterCriteria)
        {
            FilterCriteria = filterCriteria;
        }
    }
}
