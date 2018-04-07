using System;
using UrbanSpork.Tests.CQRS.Substitutes;
using UrbanSpork.CQRS.Domain;

namespace UrbanSpork.Tests.CQRS.Substitutes
{
    public class TestAggregate : AggregateRoot
    {
        private TestAggregate() { }
        public TestAggregate(Guid id)
        {
            Id = id;
            ApplyChange(new TestAggregateCreated());
        }

        public int DidSomethingCount;

        public void DoSomething()
        {
            ApplyChange(new TestAggregateDidSomething());
        }

        public void DoSomethingElse()
        {
            ApplyChange(new TestAggregateDidSomethingElse());
        }

        public void Apply(TestAggregateDidSomething e)
        {
            DidSomethingCount++;
        }
    }
}
