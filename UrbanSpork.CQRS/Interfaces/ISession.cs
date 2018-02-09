using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Interfaces.Domain;

namespace UrbanSpork.CQRS.Interfaces
{
    public interface ISession
    {
        Task Add<T>(T aggregate, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot;
        Task Commit(CancellationToken cancellationToken = default(CancellationToken));
        Task<T> Get<T>(Guid id, int? expectedVersion = null, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot;
    }
}
