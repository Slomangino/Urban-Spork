using AutoMapper;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.WriteModel.Commands.User;

namespace UrbanSpork.WriteModel.CommandHandlers.User
{
    public class UpdateSingleUserCommandHandler : ICommandHandler<UpdateSingleUserCommand, UpdateUserInformationDTO>
    {
        private readonly ISession _session;
        private readonly IMapper _mapper;

        public UpdateSingleUserCommandHandler(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        public async Task<UpdateUserInformationDTO> Handle(UpdateSingleUserCommand command)
        {
            var userAgg = await _session.Get<UserAggregate>(command.Id);

            userAgg.UpdateUserInfo(command.Input);

            await _session.Commit();

            var result = _mapper.Map<UpdateUserInformationDTO>(userAgg);

            return result;
        }
    }
}
