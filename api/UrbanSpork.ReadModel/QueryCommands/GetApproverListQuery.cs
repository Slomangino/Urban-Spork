using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetApproverListQuery : IQuery<List<ApproverListDTO>>
    {
        public GetApproverListFilterCriteria Filter;

        public GetApproverListQuery(GetApproverListFilterCriteria filter)
        {
            this.Filter = filter;
        }
    }
}
