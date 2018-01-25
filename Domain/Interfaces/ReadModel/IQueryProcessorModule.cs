using System;
using Autofac;
using Autofac.Core;

namespace UrbanSpork.Domain.Interfaces.ReadModel
{
    public class IQueryProcessorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QueryProcessor>().As<IQueryProcessor>();
        }
    }
}
