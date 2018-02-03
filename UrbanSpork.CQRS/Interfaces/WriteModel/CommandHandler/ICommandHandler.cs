using System.Threading.Tasks;

namespace UrbanSpork.CQRS.Interfaces.WriteModel
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        //Task<TResult> Handle(TCommand command);
        Task<TResult> Handle(TCommand command);
    }
}
