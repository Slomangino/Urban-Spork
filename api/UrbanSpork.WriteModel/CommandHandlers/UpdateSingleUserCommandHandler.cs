using System.Threading.Tasks;
using AutoMapper;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.DataAccess;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.WriteModel.WriteModel.Commands;
using UrbanSpork.CQRS.Domain;

namespace UrbanSpork.WriteModel.CommandHandlers
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
