using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess
{
    public interface IProjection
    {
        void ListenForEvents(IEvent @event);
    }
}
