using AutoMapper;
using System;
using UrbanSpork.CQRS.Interfaces.WriteModel;
using UrbanSpork.DataAccess.DataAccess;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.Repositories;
using UrbanSpork.CQRS.Interfaces;

namespace UrbanSpork.WriteModel
{
    public class CreateSingleUserCommandHandler : ICommandHandler<CreateSingleUserCommand, UserDTO>
    {
        private IUserRepository _userRepository;
        private IUserManager _userManager;

        public CreateSingleUserCommandHandler(IUserRepository userRepository, IUserManager userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        //fix return type
        public async Task<UserDTO> Handle(CreateSingleUserCommand command)
        {
            //var userDTO = Mapper.Map<Users>(command._input);

            var userAgg = await _userManager.CreateNewUser(command._input);
            //_userRepository.CreateUser(userDTO);
            Console.WriteLine($"User created in handle! {userAgg.firstName}");
            return userAgg;


            //var result = new UserDTO
            //{
            //    userId = 12345,
            //    firstName = "Tyler",
            //    lastName = "Hall",
            //    position = "Software Engineer",
            //    department = "Development",
            //    isActive = true,
            //};
            //return result;
            //return "CreateSingleUserCommand executed!";
        }
    }
}
