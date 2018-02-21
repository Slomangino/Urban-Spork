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
    public class UpdateSingleUserPermissionsCommandHandler : ICommandHandler<UpdateSingleUserPermissionsCommand, UserDTO>
    {
        private readonly IUserManager _userManager;

        public UpdateSingleUserPermissionsCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO> Handle(UpdateSingleUserPermissionsCommand command)
        {
            var dto = await _userManager.UpdateSingleUserPermissions(command.Input);
            return dto;
        }
    }
}
