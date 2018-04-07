using System;
using System.Threading.Tasks;
using CQRSlite.Tests.Substitutes;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.Domain.Exception;
using UrbanSpork.Tests.CQRS.Substitutes;
using Xunit;

namespace UrbanSpork.Tests.CQRS.Domain
{
    public class When_getting_events_out_of_order
    {
	    private ISession _session;

        public When_getting_events_out_of_order()
        {
            var eventStore = new TestEventStoreWithBugs();
            _session = new Session(new Repository(eventStore));
        }

        [Fact]
        public async Task Should_throw_concurrency_exception()
        {
            var id = Guid.NewGuid();
            await Assert.ThrowsAsync<EventsOutOfOrderException>(async () => await _session.Get<TestAggregate>(id, 3));
        }
    }
}