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
    public class DisablePermissionCommandHandler : ICommandHandler<DisablePermissionCommand, PermissionDTO>
    {
        private readonly IPermissionManager _permissionManager;

        public DisablePermissionCommandHandler(IPermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        public async Task<PermissionDTO> Handle(DisablePermissionCommand command)
        {
            var result = await _permissionManager.DisablePermission(command.Id);
            return result;
        }
    }
}
