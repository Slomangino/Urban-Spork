using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces.ReadModel;

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
