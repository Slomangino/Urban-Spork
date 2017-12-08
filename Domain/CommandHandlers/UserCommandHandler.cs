using CQRSlite.Commands;
using CQRSlite.Domain;
using UrbanSpork.Domain.Commands;
using UrbanSpork.Domain.WriteModel;
using System;
using System.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.CommandHandlers
{
    public class UserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly ISession _session;

        public UserCommandHandler(ISession session)
        {
            _session = session;
        }

        public void Handle(CreateUserCommand command)
        {
            User user = new User(command.Id, command.UserID, command.FirstName, command.LastName, command.Email, command.Position, command.Department, command.IsActive, command.Assets);
            _session.Add(user);
            _session.Commit();
        }
    }
}
