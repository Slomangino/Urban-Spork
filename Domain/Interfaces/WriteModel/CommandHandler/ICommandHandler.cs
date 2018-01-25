using System;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.Interfaces.WriteModel
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        //Task<TResult> Handle(TCommand command);
        Task<TResult> Handle(TCommand command);
    }
}
