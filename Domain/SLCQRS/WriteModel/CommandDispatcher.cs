using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;

namespace UrbanSpork.Domain.SLCQRS.WriteModel
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public TResult Execute<TResult>(ICommand<TResult> command)
        {
            var tCommandType = command.GetType();
            var tResultType = typeof(TResult);

            // invoke method dynamically
            MethodInfo method = typeof(CommandDispatcher).GetMethod("ExecuteInternal");
            MethodInfo generic = method.MakeGenericMethod(tCommandType, tResultType);
            object result = generic.Invoke(this, new[] { command });

            return (TResult)result;
        }

        public void ExecuteInternal<TCommand, TResult>(TCommand command)
            where TCommand : ICommand<TResult>
        {
            var handler = _context.Resolve<ICommandHandler<TCommand, TResult>>();
            //return handler.Handle(command);
            handler.Handle(command);
        }
    }
}
