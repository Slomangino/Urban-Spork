using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetAllPermissionsQuery : IQuery<List<PermissionDTO>>
    {
    }
}
