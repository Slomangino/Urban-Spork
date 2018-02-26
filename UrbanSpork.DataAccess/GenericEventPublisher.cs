using UrbanSpork.CQRS.Events;
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
        private readonly PermissionDetailProjection _permissionDetailProjection;
        private readonly PendingRequestsProjection _pendingRequestsProjection;

        public GenericEventPublisher(UserDetailProjection userDetailProjection, 
            PermissionDetailProjection permissionDetailProjection,
            PendingRequestsProjection pendingRequestsProjection)
        {
            _userDetailProjection = userDetailProjection;
            _permissionDetailProjection = permissionDetailProjection;
            _pendingRequestsProjection = pendingRequestsProjection;
        }

        async Task IEventPublisher.Publish<T>(T @event, CancellationToken cancellationToken)
        {
            //throw all events to each projection
            await _userDetailProjection.ListenForEvents(@event);
            await _permissionDetailProjection.ListenForEvents(@event);
            await _pendingRequestsProjection.ListenForEvents(@event);
        }
    }
}
