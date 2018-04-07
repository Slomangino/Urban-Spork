using System;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.Snapshotting;
using UrbanSpork.Tests.CQRS.Substitutes;
using Xunit;

namespace UrbanSpork.Tests.CQRS.Snapshotting
{
    public class When_getting_not_snapshotable_aggreate
    {
        private TestSnapshotStore _snapshotStore;
        private TestAggregate _aggregate;

        public When_getting_not_snapshotable_aggreate()
        {
            var eventStore = new TestEventStore();
            _snapshotStore = new TestSnapshotStore();
            var snapshotStrategy = new DefaultSnapshotStrategy();
            var repository = new SnapshotRepository(_snapshotStore, snapshotStrategy, new Repository(eventStore), eventStore);
            var session = new Session(repository);

            _aggregate = session.Get<TestAggregate>(Guid.NewGuid()).Result;
        }

        [Fact]
        public void Should_not_ask_for_snapshot()
        {
            Assert.False(_snapshotStore.VerifyGet);
        }

        [Fact]
        public void Should_restore_from_events()
        {
            Assert.Equal(3, _aggregate.Version);
        }
    }
}
