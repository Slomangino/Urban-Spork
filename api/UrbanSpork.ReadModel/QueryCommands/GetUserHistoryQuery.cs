using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetUserHistoryQuery : IQuery<List<UserHistoryProjection>>
    {
        public UserHistoryFilterCriteria FilterCriteria;

        public GetUserHistoryQuery(UserHistoryFilterCriteria filter)
        {
            this.FilterCriteria = filter;
        }
    }
}
