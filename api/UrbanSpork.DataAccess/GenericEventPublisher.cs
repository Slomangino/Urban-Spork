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
        private readonly PermissionTemplateProjection _permissionTemplateProjection;
        private readonly DashboardProjection _dashBoardProjection;
        private readonly UserHistoryProjection _userHistoryProjection;

        public GenericEventPublisher(
            UserDetailProjection userDetailProjection, 
            PermissionDetailProjection permissionDetailProjection, 
            PendingRequestsProjection pendingRequestsProjection,
            SystemDropdownProjection systemDropdownProjection, 
            UserManagementProjection userManagementProjection,
            SystemActivityProjection systemActivityProjection,
            ApproverActivityProjection approverActivityProjection,
            PermissionTemplateProjection permissionTemplateProjection,
            DashboardProjection dashboardProjection,
            UserHistoryProjection userHistoryProjection
            )
        {
            _userDetailProjection = userDetailProjection;
            _permissionDetailProjection = permissionDetailProjection;
            _pendingRequestsProjection = pendingRequestsProjection;
            _userManagementProjection = userManagementProjection;
            _systemDropdownProjection = systemDropdownProjection;
            _approverActivityProjection = approverActivityProjection;
            _systemActivityProjection = systemActivityProjection;
            _permissionTemplateProjection = permissionTemplateProjection;
            _dashBoardProjection = dashboardProjection;
            _userHistoryProjection = userHistoryProjection;
        }

        async Task IEventPublisher.Publish<T>(T @event, CancellationToken cancellationToken)
        {
            //throw all events to each projection

            //order matters for this particular projection because it checks from previous state
            await _dashBoardProjection.ListenForEvents(@event);

            await _userDetailProjection.ListenForEvents(@event);
            await _permissionDetailProjection.ListenForEvents(@event);
            await _pendingRequestsProjection.ListenForEvents(@event);
            await _systemDropdownProjection.ListenForEvents(@event);
            await _userManagementProjection.ListenForEvents(@event);
            await _approverActivityProjection.ListenForEvents(@event);
            await _systemActivityProjection.ListenForEvents(@event);
            await _permissionTemplateProjection.ListenForEvents(@event);
            await _userHistoryProjection.ListenForEvents(@event);
        }
    }
}
