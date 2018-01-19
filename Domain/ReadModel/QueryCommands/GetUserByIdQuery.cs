using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Domain.SLCQRS.ReadModel;

namespace UrbanSpork.Domain.ReadModel.QueryCommands
{
    public class GetUserByIdQuery : IQuery<Task<List<JObject>>>
    {
        internal int Id;

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
