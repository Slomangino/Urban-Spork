using System;

namespace UrbanSpork.Domain.SLCQRS.ReadModel
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
