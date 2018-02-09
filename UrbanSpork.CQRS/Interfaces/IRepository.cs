using System;
using System.Threading;
using System.Threading.Tasks;
using UrbanSpork.CQRS.Interfaces.Domain;

namespace CQRSlite.Domain
{
    public interface IRepository
    {
        Task<T> Get<T>(Guid aggregateId, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot;
        Task Save<T>(T aggregate, int? expectedVersion = null, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot;
    }
}