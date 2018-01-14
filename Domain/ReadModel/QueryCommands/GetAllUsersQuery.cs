using System;
using UrbanSpork.Domain.SLCQRS.ReadModel;

namespace UrbanSpork.Domain.ReadModel.QueryCommands
{
    //IQuery<String> Means that we are returning a string object.
    public class GetAllUsersQuery : IQuery<string>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
