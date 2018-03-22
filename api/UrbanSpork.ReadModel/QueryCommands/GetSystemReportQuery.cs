using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.FilterCriteria;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetSystemReportQuery : IQuery<List<SystemActivityDTO>>
    {
        public SystemReportFilterCriteria FilterCriteria { get; private set; }

        public GetSystemReportQuery(SystemReportFilterCriteria filterCriteria)
        {
            FilterCriteria = filterCriteria;
        }
    }
}
