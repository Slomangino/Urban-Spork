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

        public UrbanDbContext(DbContextOptions<UrbanDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<EventStoreDataRow> Events { get; set; }
    }
}
