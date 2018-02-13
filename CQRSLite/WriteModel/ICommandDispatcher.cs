using CQRSLite.WriteModel.Command;
using System.Threading.Tasks;

namespace CQRSLite.WriteModel
{
    public interface ICommandDispatcher
    {
        Task<TResult> Execute<TResult>(ICommand<TResult> command);
    }
}
