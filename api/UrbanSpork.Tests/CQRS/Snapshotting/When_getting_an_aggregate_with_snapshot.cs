using System;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.Snapshotting;
using UrbanSpork.Tests.CQRS.Substitutes;
using Xunit;

namespace UrbanSpork.Tests.CQRS.Snapshotting
{
    public class When_getting_an_aggregate_with_snapshot
    {
        private TestSnapshotAggregate _aggregate;

        public When_getting_an_aggregate_with_snapshot()
        {
            var eventStore = new TestInMemoryEventStore();
            var snapshotStore = new TestSnapshotStore();
            var snapshotStrategy = new DefaultSnapshotStrategy();
            var snapshotRepository = new SnapshotRepository(snapshotStore, snapshotStrategy, new Repository(eventStore), eventStore);
            var session = new Session(snapshotRepository);

            _aggregate = session.Get<TestSnapshotAggregate>(Guid.NewGuid()).Result;
        }

        [Fact]
        public void Should_restore()
        {
            Assert.True(_aggregate.Restored);
        }
    }
}
