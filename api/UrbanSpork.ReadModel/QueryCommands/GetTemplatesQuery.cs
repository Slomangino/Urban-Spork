using System.Collections.Generic;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetTemplatesQuery : IQuery<List<PermissionTemplateProjection>>
    {
        
    }
}