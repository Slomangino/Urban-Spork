using System;
using System.Collections.Generic;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetPendingRequestsByIdQuery : IQuery<List<PendingRequestsProjection>>
    {
        public Guid Id { get; set; }
    }
}