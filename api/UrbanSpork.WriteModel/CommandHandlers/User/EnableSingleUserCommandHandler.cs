using AutoMapper;
using System.Threading.Tasks;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.DataAccess;
using UrbanSpork.WriteModel.Commands.User;

namespace UrbanSpork.WriteModel.CommandHandlers.User
{
    public class EnableSingleUserCommandHandler : ICommandHandler<EnableSingleUserCommand, UserDTO>
    {
        private readonly ISession _session;
        private readonly IMapper _mapper;

        public EnableSingleUserCommandHandler(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(EnableSingleUserCommand command)
        {
            var userAgg = await _session.Get<UserAggregate>(command.Input.UserId);

            if (!userAgg.IsActive)
            {
                userAgg.EnableSingleUser(await _session.Get<UserAggregate>(command.Input.ById));
                await _session.Commit();
            }
            var result = _mapper.Map<UserDTO>(userAgg);
            return result;
        }
    }
}
