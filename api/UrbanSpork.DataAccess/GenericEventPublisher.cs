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
        private readonly SystemActivityProjection _systemActivityProjection;
        private readonly DashboardProjection _dashBoardProjection;

        public GenericEventPublisher(
            UserDetailProjection userDetailProjection, 
            PermissionDetailProjection permissionDetailProjection, 
            PendingRequestsProjection pendingRequestsProjection,
            SystemDropdownProjection systemDropdownProjection, 
            UserManagementProjection userManagementProjection,
            SystemActivityProjection systemActivityProjection,
            ApproverActivityProjection approverActivityProjection,
            DashboardProjection dashboardProjection
            )
        {
            _userDetailProjection = userDetailProjection;
            _permissionDetailProjection = permissionDetailProjection;
            _pendingRequestsProjection = pendingRequestsProjection;
            _userManagementProjection = userManagementProjection;
            _systemDropdownProjection = systemDropdownProjection;
            _approverActivityProjection = approverActivityProjection;
            _systemActivityProjection = systemActivityProjection;
            _dashBoardProjection = dashboardProjection;
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
            await _systemActivityProjection.ListenForEvents(@event);
            await _dashBoardProjection.ListenForEvents(@event);
        }
    }
}
