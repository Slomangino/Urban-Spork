using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetApproverActicityProjectionQuery : IQuery<List<ApproverActivityProjection>>
    {
        public ApproverActivityFilterCriteria FilterCriteria;
    }
}
