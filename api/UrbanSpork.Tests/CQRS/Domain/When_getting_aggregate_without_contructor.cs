using System;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.Domain.Exception;
using UrbanSpork.Tests.CQRS.Substitutes;
using Xunit;

namespace UrbanSpork.Tests.CQRS.Domain
{
    public class When_getting_aggregate_without_contructor
    {
	    private Guid _id;
	    private readonly Repository _repository;

        public When_getting_aggregate_without_contructor()
        {
            _id = Guid.NewGuid();
            var eventStore = new TestInMemoryEventStore();
            _repository = new Repository(eventStore);
            var aggreagate = new TestAggregateNoParameterLessConstructor(1, _id);
            aggreagate.DoSomething();
            Task.Run(() => _repository.Save(aggreagate)).Wait();
        }

        [Fact]
        public async Task Should_throw_missing_parameterless_constructor_exception()
        {
            await Assert.ThrowsAsync<MissingParameterLessConstructorException>(async () => await _repository.Get<TestAggregateNoParameterLessConstructor>(_id));
        }
    }
}