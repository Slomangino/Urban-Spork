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
    public class RevokeUserPermissionCommandHandler : ICommandHandler<RevokeUserPermissionCommand, UserDTO>
    {
        private readonly IUserManager _userManager;

        public RevokeUserPermissionCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO> Handle(RevokeUserPermissionCommand command)
        {
            var result = await _userManager.RevokePermissions(command.Input);
            return result;
        }
    }
}
