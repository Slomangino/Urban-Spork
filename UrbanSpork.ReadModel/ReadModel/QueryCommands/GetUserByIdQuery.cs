using CQRSLite.Queries.Query;
using System;
using UrbanSpork.Common.DataTransferObjects;

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
