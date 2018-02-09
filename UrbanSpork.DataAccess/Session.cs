//using Autofac;
//using CQRSlite.Events;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using UrbanSpork.Common;
//using UrbanSpork.Common.DataTransferObjects;
//using UrbanSpork.CQRS.Interfaces;
//using UrbanSpork.CQRS.Interfaces.Domain;
//using UrbanSpork.CQRS.Interfaces.Events;
//using UrbanSpork.DataAccess.DataAccess;

//namespace UrbanSpork.DataAccess
//{
//    public class Session : ISession
//    {
//        private readonly UrbanDbContext _context;
//        private readonly IComponentContext _componentContext;

//        public Session (UrbanDbContext context, IComponentContext componentContext)
//        {
//            _context = context;
//            _componentContext = componentContext;
//        }

//        public Task Add<T>(T aggregate, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot
//        {
//            throw new NotImplementedException();
//        }

//        public Task Commit(CancellationToken cancellationToken = default(CancellationToken))
//        {
//            throw new NotImplementedException();
//        }

//        public Task<T> Get<T>(Guid id, int? expectedVersion = null, CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot
//        {
//            var rowList = _context.Events.ToList().Where(a => a.Id.Equals(id));
//            var eventList = DeserializeEventList(rowList);
//            var aggregate = new UserAggregate(eventList);
             
//            return Task.FromResult(aggregate); // how do?
//        }

//        private IEnumerable<IEvent> DeserializeEventList(IEnumerable<EventStoreDataRow> rowList)
//        {
//            var events = new List<IEvent>();
//            foreach (EventStoreDataRow dr in rowList)
//            {
//                Type type = Type.GetType(dr.EventType);
//                var @event = Activator.CreateInstance(type);
//                JsonConvert.PopulateObject(dr.EventData, @event);
//                events.Add((IEvent)@event);
//            }
//            return events;
//        }
//    }
//}
