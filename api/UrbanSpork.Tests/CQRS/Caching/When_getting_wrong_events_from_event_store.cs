﻿using System;
using System.Threading.Tasks;
using CQRSlite.Tests.Substitutes;
using UrbanSpork.CQRS.Caching;
using UrbanSpork.Tests.CQRS.Substitutes;
using Xunit;

namespace UrbanSpork.Tests.CQRS.Caching
{
    public class When_getting_earlier_than_expected_events_from_event_store
    {
        private CacheRepository _rep;
        private TestAggregate _aggregate;
        private ICache _cache;

        public When_getting_earlier_than_expected_events_from_event_store()
        {
            _cache = new MemoryCache();
            _rep = new CacheRepository(new TestRepository(), new TestEventStoreWithBugs(), _cache);
            _aggregate = _rep.Get<TestAggregate>(Guid.NewGuid()).Result;
        }

        [Fact]
        public async Task Should_evict_old_object_from_cache()
        {
            await _rep.Get<TestAggregate>(_aggregate.Id);
            var aggregate = await _cache.Get(_aggregate.Id);
            Assert.NotEqual(_aggregate, aggregate);
        }

        [Fact]
        public async Task Should_get_events_from_start()
        {
            var aggregate = await _rep.Get<TestAggregate>(_aggregate.Id);
            Assert.Equal(1, aggregate.Version);
        }
    }
}