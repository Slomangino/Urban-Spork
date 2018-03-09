using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class DisableSingleUserCommandHandler : ICommandHandler<DisableSingleUserCommand, UserDTO>
    {
        private readonly IUserManager _userManager;

        public DisableSingleUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO> Handle(DisableSingleUserCommand command)
        {
            var dto = await _userManager.DisableSingleUser(command.Id);
            return dto;
        }
    }
}
