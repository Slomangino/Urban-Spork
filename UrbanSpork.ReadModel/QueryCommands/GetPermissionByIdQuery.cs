using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetPermissionByIdQuery : IQuery<PermissionDTO>
    {
        public Guid Id;
        public GetPermissionByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
