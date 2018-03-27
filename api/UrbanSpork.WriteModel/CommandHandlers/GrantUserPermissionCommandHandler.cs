using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class GrantUserPermissionCommandHandler : ICommandHandler<GrantUserPermissionCommand, UserDTO>
    {
        private readonly IUserManager _userManager;

        public GrantUserPermissionCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO> Handle(GrantUserPermissionCommand command)
        {
            var result = await _userManager.GrantUserPermission(command.Input);
            return result;
        }
    }
}
