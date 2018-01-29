using AutoMapper;
using System;
using UrbanSpork.Domain.Interfaces.WriteModel;
using UrbanSpork.DataAccess.DataAccess;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.DataAccess.Repositories;

namespace UrbanSpork.DataAccess.WriteModel
{
    public class CreateSingleUserCommandHandler : ICommandHandler<CreateSingleUserCommand, UserDTO>
    {
        private IUserRepository _userRepository;

        public CreateSingleUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //fix return type
        public Task<UserDTO> Handle(CreateSingleUserCommand command)
        {
            var userDTO = Mapper.Map<Users>(command._input);

            _userRepository.CreateUser(userDTO);

            return Task.FromResult(command._input);


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
