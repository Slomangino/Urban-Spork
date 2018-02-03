using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.CQRS.Interfaces.Events;
using UrbanSpork.DataAccess.Events.Users;

namespace UrbanSpork.DataAccess.DataAccess
{
    public class UrbanDbContext : DbContext
    {
        public DbSet<User> Users2 { get; set; }
        public DbSet<EventStoreDataRow> Events { get; set; }

        public UrbanDbContext(DbContextOptions<UrbanDbContext> options) : base(options)
        {
        }
    }
}
