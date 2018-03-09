using System;

namespace UrbanSpork.CQRS.Routing.Exception
{
    public class HandlerNotResolvedException : ArgumentNullException
    {
        public HandlerNotResolvedException(string paramName)
            : base($"Type {paramName} was resolved to null from servicelocator")
        {
        }
    }
}
