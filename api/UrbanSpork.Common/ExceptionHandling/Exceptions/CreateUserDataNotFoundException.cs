using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Common.ExceptionHandling.Exceptions
{
    public class CreateUserDataNotFoundException : Exception
    {
        public CreateUserDataNotFoundException()
        {
        }

        public CreateUserDataNotFoundException(string message)
            : base(message)
        {
        }
    }
}
