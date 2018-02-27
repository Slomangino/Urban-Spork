using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using UrbanSpork.CQRS.Domain;
using UrbanSpork.CQRS.Events;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.DataAccess;
using UrbanSpork.DataAccess.Events;

namespace UrbanSpork.API.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private UrbanDbContext _context;
        private IEventPublisher _publisher;
        public static Assembly myAss;

        public EventController(UrbanDbContext context, IEventPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        [HttpGet]
        public async Task Rebuild()
        {
            var eventStore = _context.Events;

            string assemblyThatINeed = "UrbanSpork.DataAccess";
            var assembly = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var t in assembly)
            {
                if (t.FullName.StartsWith(assemblyThatINeed))
                {
                    myAss = t;
                }
            }

            var orderedEventList = DeserializeEventList(eventStore);

            //foreach (var @event in orderedEventList)
            //{
            //    Console.WriteLine(@event.Id + " " + @event.Version + " " + @event.TimeStamp);
            //}


            foreach (var @event in orderedEventList)
            {
                Console.WriteLine(@event.Id + "_" + @event.Version + "_" + @event.TimeStamp);
                try
                {
                    await _publisher.Publish(@event);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            Console.WriteLine("Done, Press key yo.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private IEvent DeserializeEvent(EventStoreDataRow row)
        {
            Type type = myAss.GetType(row.EventType);
            var @event = Activator.CreateInstance(type);
            JsonConvert.PopulateObject(row.EventData, @event);
            return (IEvent)@event;
        }

        private IEnumerable<IEvent> DeserializeEventList(IEnumerable<EventStoreDataRow> rowList)
        {
            var events = new List<IEvent>();
            foreach (EventStoreDataRow dr in rowList)
            {
                Type type = myAss.GetType(dr.EventType);
                var @event = Activator.CreateInstance(type);
                JsonConvert.PopulateObject(dr.EventData, @event);
                events.Add((IEvent)@event);
            }
            return events.OrderBy(a => a.TimeStamp);
        }
    }
}