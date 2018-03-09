using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class CreateSingleUserCommandHandler : ICommandHandler<CreateSingleUserCommand, UserDTO>
    {
        private readonly IUserManager _userManager;

        public CreateSingleUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO> Handle(CreateSingleUserCommand command)
        {
            var userDTO = await _userManager.CreateNewUser(command.Input);
            return userDTO;
        }
    }
}
