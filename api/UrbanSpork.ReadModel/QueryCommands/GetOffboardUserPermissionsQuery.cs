using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetOffboardUserPermissionsQuery : IQuery<OffBoardUserDTO>
    {
        public Guid UserID;
       

        public GetOffboardUserPermissionsQuery(Guid id)
        {
            UserID = id;
        }
    }
}
