using System.Threading.Tasks;
using UrbanSpork.CQRS.WriteModel.Command;

namespace UrbanSpork.CQRS.WriteModel.CommandHandler
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        //Task<TResult> Handle(TCommand command);
        Task<TResult> Handle(TCommand command);
    }
}
