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
    public class UpdateUserPermissionsCommandHandler : ICommandHandler<UpdateUserPermissionsCommand, UserDTO>
    {
        private readonly IUserManager _userManager;

        public UpdateUserPermissionsCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<UserDTO> Handle(UpdateUserPermissionsCommand command)
        {
            return _userManager.UpdateUserPermissions(command.Input);
        }
    }
}
