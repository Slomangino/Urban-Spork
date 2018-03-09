using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetUserDetailByIdQuery : IQuery<UserDetailProjectionDTO>
    {
        internal Guid Id;

        public GetUserDetailByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
