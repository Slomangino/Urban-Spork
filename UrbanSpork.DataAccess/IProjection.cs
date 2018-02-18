using System.Threading.Tasks;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.DataAccess
{
    public interface IProjection
    {
        Task ListenForEvents(IEvent @event);
    }
}
