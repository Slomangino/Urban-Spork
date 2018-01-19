using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.DataAccess.DataAccess
{
    public class UrbanDbContext : DbContext
    {
        public UrbanDbContext(DbContextOptions<UrbanDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
