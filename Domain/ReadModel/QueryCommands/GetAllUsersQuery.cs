using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.Domain.Interfaces.ReadModel;

namespace UrbanSpork.Domain.ReadModel.QueryCommands
{
    //IQuery<String> Means that we are returning a string object.
    public class GetAllUsersQuery : IQuery<List<UserDTO>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
