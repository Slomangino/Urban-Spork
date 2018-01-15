using System;

namespace UrbanSpork.Domain.SLCQRS.WriteModel
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        TResult Handle(TCommand command);
    }
}
