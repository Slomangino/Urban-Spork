using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class UserPermissionsRequestedCommandHandler : ICommandHandler<UserPermissionsRequestedCommand, UserDTO>
    {
        private readonly IUserManager _userManager;

        public UserPermissionsRequestedCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<UserDTO> Handle(UserPermissionsRequestedCommand requestedCommand)
        {
            return _userManager.UserPermissionsRequested(requestedCommand.Input);
        }
    }
}
