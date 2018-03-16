using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetAllPositionsQuery : IQuery<List<PositionProjection>>
    {
    }
}
