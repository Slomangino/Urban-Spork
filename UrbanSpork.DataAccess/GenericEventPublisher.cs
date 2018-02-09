using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.DataAccess
{
    public class GenericEventPublisher : IEventPublisher
    {
        //list of projection tables
        private readonly UserDetailProjection _userDetailProjection;   

        public GenericEventPublisher(UserDetailProjection userDetailProjection)
        {
            _userDetailProjection = userDetailProjection;
        }

        Task IEventPublisher.Publish<T>(T @event, CancellationToken cancellationToken)
        {
            //throw all events to each projection
            _userDetailProjection.ListenForEvents(@event);

            return Task.FromResult(0);
        }
    }
}
