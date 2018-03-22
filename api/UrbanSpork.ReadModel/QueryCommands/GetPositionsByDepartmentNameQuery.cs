using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetPositionsByDepartmentNameQuery : IQuery<List<PositionProjection>>
    {
        public string departmentName;

        public GetPositionsByDepartmentNameQuery(string DepartmentName)
        {
            departmentName = DepartmentName;
        }
    }
}
