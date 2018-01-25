using System;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.SLCQRS.WriteModel
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        //Task<TResult> Handle(TCommand command);
        void Handle(TCommand command);
    }
}
