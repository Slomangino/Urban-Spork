using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.DataAccess;
using UrbanSpork.CQRS.WriteModel.CommandHandler;
using UrbanSpork.WriteModel.Commands.User;
using UrbanSpork.CQRS.Domain;

namespace UrbanSpork.WriteModel.CommandHandlers.User
{
    public class DisableSingleUserCommandHandler : ICommandHandler<DisableSingleUserCommand, UserDTO>
    {
        private readonly ISession _session;
        private readonly IMapper _mapper;

        public DisableSingleUserCommandHandler(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(DisableSingleUserCommand command)
        {
            //var dto = await _userManager.DisableSingleUser(command.Input);
            //return dto;

            var userAgg = await _session.Get<UserAggregate>(command.Input.UserId);

            if (userAgg.IsActive)
            {
                userAgg.DisableSingleUser(await _session.Get<UserAggregate>(command.Input.ById));
                await _session.Commit();
            }
            var result = _mapper.Map<UserDTO>(userAgg);
            return result;
        }
    }
}
