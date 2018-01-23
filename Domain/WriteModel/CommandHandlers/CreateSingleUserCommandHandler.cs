using AutoMapper;
using System;
using UrbanSpork.Domain.WriteModel.Commands;
using UrbanSpork.Domain.SLCQRS.WriteModel;
using UrbanSpork.DataAccess.DataAccess;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.DataAccess.Repositories;

namespace UrbanSpork.Domain.WriteModel
{
    public class CreateSingleUserCommandHandler : ICommandHandler<CreateSingleUserCommand, UserDTO>
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;

        public CreateSingleUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public void Handle(CreateSingleUserCommand command)
        {
            var userDTO = _mapper.Map<Users>(command._input);

            _userRepository.CreateUser(userDTO);


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
