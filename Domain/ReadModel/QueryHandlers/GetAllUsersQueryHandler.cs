using System;
using Autofac;
using UrbanSpork.Domain.ReadModel.QueryCommands;
using UrbanSpork.Domain.SLCQRS.ReadModel;

namespace UrbanSpork.Domain.ReadModel.QueryHandlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, string>
    {

        public GetAllUsersQueryHandler()
        {
        }

        public string Handle(GetAllUsersQuery query)
        {
            return "Stephen!";
        }
    }
}
