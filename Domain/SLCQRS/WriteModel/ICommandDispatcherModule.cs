using System;
using Autofac;
using Autofac.Core;

namespace UrbanSpork.Domain.SLCQRS.WriteModel
{
    public class ICommandDispatcherModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
        }
    }
}
