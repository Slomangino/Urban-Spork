using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.WriteModel.Commands
{
    public class CreateSingleUserCommand : ICommand<UserDTO>
    {
        public UserInputDTO Input { get; set; }

        public CreateSingleUserCommand(UserInputDTO input)
        {
            Input = input;
        }
    }
}
