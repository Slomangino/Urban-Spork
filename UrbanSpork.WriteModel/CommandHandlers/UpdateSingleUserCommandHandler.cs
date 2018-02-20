using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.WriteModel.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class UpdateSingleUserCommandHandler : ICommandHandler<UpdateSingleUserCommand, UserDTO>
    {
        private IUserManager _userManager;

        public UpdateSingleUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        //fix return type
        public async Task<UserDTO> Handle(UpdateSingleUserCommand command)
        {
            var userAgg = await _userManager.UpdateUser(command.id, command.userInputDTO);
            return userAgg;
        }
    }
}
