using UrbanSpork.CQRS.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UrbanSpork.DataAccess.DataAccess;

namespace UrbanSpork.DataAccess.Events
{
    public class EventStore : IEventStore
    {
        private readonly UrbanDbContext _context;

        public EventStore(UrbanDbContext context, IEventPublisher publisher)
        {
            _context = context;
        }

        public Task<IEnumerable<IEvent>> Get(Guid aggregateId, int fromVersion, CancellationToken cancellationToken = default(CancellationToken))
        {
            var rowList = _context.Events.ToList().Where(a => a.Id.Equals(aggregateId) && a.Version > fromVersion);
            var eventList = DeserializeEventList(rowList);

            return Task.FromResult(eventList);
        }

        public async Task Save(IEnumerable<IEvent> events, CancellationToken cancellationToken = default(CancellationToken))
        {
                foreach (IEvent e in events)
                {
                    var serializedEvent = BuildEventRowFromEvent(e);
                    await _context.Events.AddAsync(serializedEvent, cancellationToken);
                }
                await _context.SaveChangesAsync(cancellationToken);
        }

        private IEnumerable<IEvent> DeserializeEventList(IEnumerable<EventStoreDataRow> rowList)
        {
            var events = new List<IEvent>();
            foreach (EventStoreDataRow dr in rowList)
            {
                Type type = Type.GetType(dr.EventType);
                var @event = Activator.CreateInstance(type);
                JsonConvert.PopulateObject(dr.EventData, @event);
                events.Add((IEvent)@event);
            }
            return events;
        }

        private EventStoreDataRow BuildEventRowFromEvent(IEvent e)
        {
            var eventData = new EventStoreDataRow
            {
                Id = e.Id,
                Version = e.Version,
                EventType = e.GetType().FullName,
                EventData = SerializeEvent(e)
            };
            return eventData;
        }

        private string SerializeEvent(IEvent e)
        {
            return JsonConvert.SerializeObject(e);
        }
    }
}
