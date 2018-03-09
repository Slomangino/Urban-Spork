using System;
using UrbanSpork.CQRS.Domain.Exception;

namespace UrbanSpork.CQRS.Domain.Factories
{
    internal static class AggregateFactory
    {
        public static T CreateAggregate<T>()
        {
            try
            {
#if NETSTANDARD1_3
                return (T)Activator.CreateInstance(typeof(T));
#else
                return (T)Activator.CreateInstance(typeof(T), true);
#endif
            }
            catch (MissingMethodException)
            {
                throw new MissingParameterLessConstructorException(typeof(T));
            }
        }
    }
}