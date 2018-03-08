using System.Threading;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.DataAccess
{
    public class GenericEventPublisher : IEventPublisher
    {
        //list of projection tables
        private readonly UserDetailProjection _userDetailProjection;
        private readonly PermissionDetailProjection _permissionDetailProjection;
        private readonly PendingRequestsProjection _pendingRequestsProjection;
        private readonly SystemDropdownProjection _systemDropdownProjection;
        private readonly UserManagementProjection _userManagementProjection;
        private readonly ApproverActivityProjection _approverActivityProjection;

        public GenericEventPublisher(UserDetailProjection userDetailProjection, 
            PermissionDetailProjection permissionDetailProjection, PendingRequestsProjection pendingRequestsProjection,
            SystemDropdownProjection systemDropdownProjection, UserManagementProjection userManagementProjection, 
            ApproverActivityProjection approverActivityProjection)
        {
            _userDetailProjection = userDetailProjection;
            _permissionDetailProjection = permissionDetailProjection;
            _pendingRequestsProjection = pendingRequestsProjection;
            _userManagementProjection = userManagementProjection;
            _systemDropdownProjection = systemDropdownProjection;
            _approverActivityProjection = approverActivityProjection;
        }

        async Task IEventPublisher.Publish<T>(T @event, CancellationToken cancellationToken)
        {
            //throw all events to each projection
            await _userDetailProjection.ListenForEvents(@event);
            await _permissionDetailProjection.ListenForEvents(@event);
            await _pendingRequestsProjection.ListenForEvents(@event);
            await _systemDropdownProjection.ListenForEvents(@event);
            await _userManagementProjection.ListenForEvents(@event);
            await _approverActivityProjection.ListenForEvents(@event);
        }
    }
}
