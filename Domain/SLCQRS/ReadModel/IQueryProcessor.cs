using System;
using System.Threading.Tasks;

namespace UrbanSpork.Domain.SLCQRS.ReadModel
{
    public interface IQueryProcessor
    {
        TResult Process<TResult>(IQuery<TResult> query);
    }
}
