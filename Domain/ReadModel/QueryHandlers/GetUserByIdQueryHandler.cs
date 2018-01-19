using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.Domain.SLCQRS.ReadModel;

namespace UrbanSpork.Domain.ReadModel.QueryHandlers
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Task<List<JObject>>>
    {
        public GetUserByIdQueryHandler()
        {
            // comment
        }

        public Task<List<JObject>> Handle(GetUserByIdQuery query)
        {
            return Task.FromResult(new List<JObject>());

        }
    }
}
