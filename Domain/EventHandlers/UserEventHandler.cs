using AutoMapper;
using CQRSlite.Events;
using UrbanSpork.Domain.Events.Users;
using UrbanSpork.Domain.ReadModel;
using UrbanSpork.Domain.ReadModel.Repositories.Interfaces;
using UrbanSpork.Domain.WriteModel;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.EventHandlers
{
    public class UserEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;

        public UserEventHandler(IMapper mapper, IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public Task Handle(UserCreatedEvent message)
        {
            UserRM user = _mapper.Map<UserRM>(message);
            _userRepo.Save(user);
        }
    }
}
