using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Domain.Interfaces.Events;

namespace UrbanSpork.DataAccess.DataAccess
{
    public class UrbanDbContext : DbContext
    {

        public UrbanDbContext(DbContextOptions<UrbanDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<IEvent> UserEvents { get; set; }
    }
}
