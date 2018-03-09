using UrbanSpork.CQRS.WriteModel.Command;
using System;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;

namespace UrbanSpork.WriteModel.WriteModel.Commands
{
    public class UpdateSingleUserCommand : ICommand<UpdateUserInformationDTO>
    {
        public Guid Id;
        public UpdateUserInformationDTO Input;

        public UpdateSingleUserCommand(Guid id, UpdateUserInformationDTO input)
        {
            Id = id;
            Input = input;
        }
    }
}
