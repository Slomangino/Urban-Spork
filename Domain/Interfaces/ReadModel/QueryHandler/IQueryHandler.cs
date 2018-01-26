using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.Domain.ReadModel.QueryCommands;

namespace UrbanSpork.Domain.Interfaces.ReadModel
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query);
        
    }
}
