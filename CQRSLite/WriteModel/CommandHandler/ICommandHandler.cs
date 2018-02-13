using CQRSLite.WriteModel.Command;
using System.Threading.Tasks;

namespace CQRSLite.WriteModel.CommandHandler
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        //Task<TResult> Handle(TCommand command);
        Task<TResult> Handle(TCommand command);
    }
}
