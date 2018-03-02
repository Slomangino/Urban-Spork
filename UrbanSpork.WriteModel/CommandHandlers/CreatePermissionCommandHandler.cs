using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class CreatePermissionCommandHandler : ICommandHandler<CreatePermissionCommand, PermissionDTO>
    {
        private readonly IPermissionManager _permissionManager;

        public CreatePermissionCommandHandler(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        public async Task<PermissionDTO> Handle(CreatePermissionCommand command)
        {
            var permissionDTO = await _permissionManager.CreateNewPermission(command.Input);
            return permissionDTO;
        }
    }
}
