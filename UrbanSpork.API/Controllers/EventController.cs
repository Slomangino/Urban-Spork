using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
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
        public static Assembly myAss; //huehuehue

        public EventController(UrbanDbContext context, IEventPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        //http://localhost:5000/api/event
        [HttpGet]
        public async Task Rebuild()
        {
            //delete all data from projections while not affecting the Events table
            await DropAllProjectionData();

            var eventStore = _context.Events;

            string assemblyThatINeed = "UrbanSpork.DataAccess";
            var assembly = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var ass in assembly)
            {
                if (ass.FullName.StartsWith(assemblyThatINeed))
                {
                    myAss = ass; //huehue
                }
            }

            //returns list of events, ordered by the timestamp
            var orderedEventList = DeserializeEventList(eventStore);

            //publish each event 
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
            //Environment.Exit(0);
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

        private async Task DropAllProjectionData()
        { 
            /*
             ***********************************************************************************************************************
             * NEVER EVER EVER EVER ADD THE *EVENTS* TABLE TO THIS LIST, IT WILL DELETE ALL ROWS FROM ANY TABLE LISTED HERE!!!!!!! *
             ***********************************************************************************************************************
             * DO: add all projections to this list that need to be rebuilt when new events are added
             */

            //truncate all projections (Deletes all rows)
            await _context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE UserDetailProjection");
            await _context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE PermissionDetailProjection");
            await _context.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE PendingRequestsProjection");
        }
    }
}