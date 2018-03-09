using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Queries.Query;

namespace UrbanSpork.ReadModel.QueryCommands
{
    public class GetUserByIdQuery : IQuery<UserDTO>
    {
        internal Guid Id;

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
