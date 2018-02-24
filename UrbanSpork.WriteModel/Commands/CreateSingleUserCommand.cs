using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class CreateSingleUserCommand : ICommand<UserDTO>
    {
        public CreateUserInputDTO Input { get; set; }

        public CreateSingleUserCommand(CreateUserInputDTO input)
        {
            Input = input;
        }
    }
}
