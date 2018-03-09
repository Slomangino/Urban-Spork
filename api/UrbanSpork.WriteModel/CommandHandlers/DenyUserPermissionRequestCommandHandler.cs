using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class DenyUserPermissionRequestCommandHandler : ICommandHandler<DenyUserPermissionRequestCommand, UserDTO>
    {
        private readonly IUserManager _userManager;

        public DenyUserPermissionRequestCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO> Handle(DenyUserPermissionRequestCommand command)
        {
            var result = await _userManager.DenyUserPermissionRequest(command.Input);
            return result;
        }
    }
}
