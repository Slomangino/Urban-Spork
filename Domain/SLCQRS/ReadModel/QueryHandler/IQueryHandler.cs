using System;
using SLCQRS.ReadModel.Query;

namespace SLCQRS.ReadModel.QueryHandler
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
