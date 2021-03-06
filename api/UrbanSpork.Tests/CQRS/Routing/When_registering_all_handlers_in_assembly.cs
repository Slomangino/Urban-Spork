﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Tests.Substitutes;
using UrbanSpork.CQRS.Routing;
using UrbanSpork.CQRS.Routing.Exception;
using UrbanSpork.Tests.CQRS.Substitutes;
using Xunit;

namespace UrbanSpork.Tests.CQRS.Routing
{
    public class When_registering_all_handlers_in_assembly
    {
        private readonly TestServiceLocator _locator;
        private TestHandleRegistrar _testHandleRegistrar;

        public When_registering_all_handlers_in_assembly()
        {
            _testHandleRegistrar = new TestHandleRegistrar();
            _locator = new TestServiceLocator(_testHandleRegistrar);
            var register = new RouteRegistrar(_locator);
            register.Register(GetType()); // Changed From RegisterINAssemblyOf to Register to match apparent changes in source file
        }

        [Fact]
        public void Should_register_all_handlers()
        {
            Assert.Equal(8, _testHandleRegistrar.HandlerList.Count);
        }

        [Fact]
        public async Task Should_be_able_to_run_all_handlers()
        {
            foreach (var item in _testHandleRegistrar.HandlerList)
            {
                var eventtypes = GetEventTypesMatching(item.Type);
                foreach (var type in eventtypes)
                {
                    var @event = Activator.CreateInstance(type);
#if NETCOREAPP1_0
                    try
                    {
                        await item.Handler(@event, new CancellationToken());
                    }
                    //.NET Core 1.0 version of the library does not support explict interfaces, so these exceptions are expected
                    catch (ResolvedHandlerMethodNotFoundException)
                    {
                        Assert.Equal(typeof(TestAggregateDoSomething), item.Type);
                    }
                    catch (NotImplementedException)
                    {
                        Assert.Equal(typeof(TestAggregateDoSomething), item.Type);
                    }
#else
                    await item.Handler(@event, new CancellationToken());
                    foreach (var handler in _locator.Handlers)
                    {
                        Assert.Equal(1, handler.TimesRun);
                    }
#endif
                }
            }
            Assert.Equal(11, _locator.Handlers.Count);
        }

        [Fact]
        public async Task Unresolved_handlers_should_throw()
        {
            _locator.ReturnNull = true;
            foreach (var item in _testHandleRegistrar.HandlerList)
            {
                var eventtypes = GetEventTypesMatching(item.Type);
                foreach (var type in eventtypes)
                {
                    var @event = Activator.CreateInstance(type);
                    await Assert.ThrowsAsync<HandlerNotResolvedException>(async () =>
                    {
                        await item.Handler(@event, new CancellationToken());
                    });
                }
            }
        }

        private IEnumerable<Type> GetEventTypesMatching(Type type)
        {
            return GetType().GetTypeInfo().Assembly.GetTypes().Where(x => type.GetTypeInfo().IsAssignableFrom(x));
        }
    }
}
