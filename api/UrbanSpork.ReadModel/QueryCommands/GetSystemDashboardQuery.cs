using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.CQRS.Queries.QueryHandler;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetSystemDashboardQuery : IQuery<List<DashboardProjection>>
    {
        private readonly DashboardFilterCriteria filter;

        public GetSystemDashboardQuery(DashboardFilterCriteria filter)
        {
            this.filter = filter;
        }
    }
}
