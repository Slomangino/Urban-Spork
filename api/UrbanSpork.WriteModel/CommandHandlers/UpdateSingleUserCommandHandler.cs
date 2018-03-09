using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.DataAccess;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.WriteModel.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class UpdateSingleUserCommandHandler : ICommandHandler<UpdateSingleUserCommand, UpdateUserInformationDTO>
    {
        private IUserManager _userManager;

        public UpdateSingleUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        //fix return type
        public async Task<UpdateUserInformationDTO> Handle(UpdateSingleUserCommand command)
        {
            var userAgg = await _userManager.UpdateUserInfo(command.Id, command.Input);
            return userAgg;
        }
    }
}
