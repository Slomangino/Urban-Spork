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
    public class EnableSingleUserCommandHandler : ICommandHandler<EnableSingleUserCommand, UserDTO>
    {
        private readonly IUserManager _userManager;

        public EnableSingleUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO> Handle(EnableSingleUserCommand command)
        {
            var dto = await _userManager.EnableSingleUser(command.id);
            return dto;
        }
    }
}
