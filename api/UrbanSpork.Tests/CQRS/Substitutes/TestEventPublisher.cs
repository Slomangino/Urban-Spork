using System.Threading;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Events;

namespace UrbanSpork.Tests.CQRS.Substitutes
{
    public class TestEventPublisher : IEventPublisher
    {
        public Task Publish<T>(T @event, CancellationToken cancellationToken = default(CancellationToken)) where T : class, IEvent
        {
            Published++;
            Token = cancellationToken;
            return Task.CompletedTask;
        }

        public CancellationToken Token { get; set; }
        public int Published { get; private set; }
    }
}