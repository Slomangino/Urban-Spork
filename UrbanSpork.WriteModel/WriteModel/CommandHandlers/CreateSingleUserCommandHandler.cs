using AutoMapper;
using System;
using UrbanSpork.CQRS.Interfaces.WriteModel;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Interfaces;
using UrbanSpork.DataAccess.Repositories;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class CreateSingleUserCommandHandler : ICommandHandler<CreateSingleUserCommand, UserDTO>
    {
        
        private IUserManager _userManager;

        public CreateSingleUserCommandHandler(IUserManager userManager)
        {
           
            _userManager = userManager;
        }

        public async Task<UserDTO> Handle(CreateSingleUserCommand command)
        {
            var userDTO = await _userManager.CreateNewUser(command._input);
            Console.WriteLine($"User created in handle! {userDTO.firstName}");
            return userDTO;
        }
    }
}
