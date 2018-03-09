using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetSystemActivityReportQuery : IQuery<List<SystemActivityDTO>>
    {
        public SystemActivityReportFilterCriteria FilterCriteria { get; private set; }

        public GetSystemActivityReportQuery(SystemActivityReportFilterCriteria filterCriteria)
        {
            FilterCriteria = filterCriteria;
        }
    }
}
