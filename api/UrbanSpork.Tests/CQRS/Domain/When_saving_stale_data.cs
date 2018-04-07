using System;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.Domain.Exception;
using UrbanSpork.Tests.CQRS.Substitutes;
using Xunit;

namespace UrbanSpork.Tests.CQRS.Domain
{
    public class When_saving_stale_data
    {
        private TestInMemoryEventStore _eventStore;
        private TestAggregate _aggregate;
        private Repository _rep;
        private Session _session;


        public When_saving_stale_data()
        {
            _eventStore = new TestInMemoryEventStore();
            _rep = new Repository(_eventStore);
            _session = new Session(_rep);

            _aggregate = new TestAggregate(Guid.NewGuid());
            _aggregate.DoSomething();
            Task.Run(() => _rep.Save(_aggregate)).Wait();
        }

        [Fact]
        public async Task Should_throw_concurrency_exception_from_repository()
        {
            await Assert.ThrowsAsync<ConcurrencyException>(async () => await _rep.Save(_aggregate, 0));
        }

        [Fact]
        public async Task Should_throw_concurrency_exception_from_session()
        {
            await _session.Add(_aggregate);
            _aggregate.DoSomething();
            await _rep.Save(_aggregate);
            await Assert.ThrowsAsync<ConcurrencyException>(async () => await _session.Commit());
        }
    }
}