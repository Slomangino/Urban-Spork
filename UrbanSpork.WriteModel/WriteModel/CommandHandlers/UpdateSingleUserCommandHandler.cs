using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces;
using UrbanSpork.CQRS.Interfaces.WriteModel;
using UrbanSpork.WriteModel.WriteModel.Commands;

namespace UrbanSpork.WriteModel.WriteModel.CommandHandlers
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


            var userAgg = await _userManager.UpdateUser(command.user);

            Console.WriteLine($"User updated in handle! {userAgg.firstName}");
            return userAgg;

        }
    }
}
