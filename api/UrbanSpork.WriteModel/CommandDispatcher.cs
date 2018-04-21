using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.CQRS.WriteModel.CommandHandler;

namespace UrbanSpork.WriteModel
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task<TResult> Execute<TResult>(ICommand<TResult> command)
        //public async Task<TResult> Execute<TResult>(ICommand<TResult> command)
        {
            var tCommandType = command.GetType();
            var tResultType = typeof(TResult);

            // invoke method dynamically
            MethodInfo method = typeof(CommandDispatcher).GetMethod("ExecuteInternal");
            MethodInfo generic = method.MakeGenericMethod(tCommandType, tResultType);
            object result = generic.Invoke(this, new[] { command });

            return await (Task<TResult>)result;
            //return await (Task<TResult>)result;
        }

        public async Task<TResult> ExecuteInternal<TCommand, TResult>(TCommand command)
            where TCommand : ICommand<TResult>
        {
            var handler = _context.Resolve<ICommandHandler<TCommand, TResult>>();
            return await handler.Handle(command);
        }
    }
}
