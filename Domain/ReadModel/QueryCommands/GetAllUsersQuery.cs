using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.Domain.DataTransferObjects;
using UrbanSpork.Domain.SLCQRS.ReadModel;

namespace UrbanSpork.Domain.ReadModel.QueryCommands
{
    //IQuery<String> Means that we are returning a string object.
    public class GetAllUsersQuery : IQuery<Task<List<UserDTO>>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
