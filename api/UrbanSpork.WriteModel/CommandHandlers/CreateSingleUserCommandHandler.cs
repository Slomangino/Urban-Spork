using System.Threading.Tasks;
using AutoMapper;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.DataAccess;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess.Emails;
using UrbanSpork.WriteModel.Commands;

namespace UrbanSpork.WriteModel.CommandHandlers
{
    public class CreateSingleUserCommandHandler : ICommandHandler<CreateSingleUserCommand, UserDTO>
    {
        private readonly ISession _session;
        private readonly IEmail _email;
        private readonly IMapper _mapper;

        public CreateSingleUserCommandHandler(ISession session, IEmail email, IMapper mapper)
        {
            _session = session;
            _email = email;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(CreateSingleUserCommand command)
        {
            var userAgg = UserAggregate.CreateNewUser(command.Input);
            await _session.Add(userAgg);
            await _session.Commit();
            var userDTO = _mapper.Map<UserDTO>(await _session.Get<UserAggregate>(userAgg.Id));

            _email.SendUserCreatedMessage(userDTO);
            return userDTO;
        }
    }
}
