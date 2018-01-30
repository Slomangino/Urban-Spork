using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using UrbanSpork.CQRS.Interfaces.WriteModel;

namespace UrbanSpork.WriteModel
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            var tCommandType = command.GetType();
            var tResultType = typeof(TResult);

            // invoke method dynamically
            MethodInfo method = typeof(CommandDispatcher).GetMethod("ExecuteInternal");
            MethodInfo generic = method.MakeGenericMethod(tCommandType, tResultType);
            object result = generic.Invoke(this, new[] { command });

            return (Task<TResult>)result;
        }

        public Task<TResult> ExecuteInternal<TCommand, TResult>(TCommand command)
            where TCommand : ICommand<TResult>
        {
            var handler = _context.Resolve<ICommandHandler<TCommand, TResult>>();
            //return handler.Handle(command);
            return handler.Handle(command);
        }
    }
}
