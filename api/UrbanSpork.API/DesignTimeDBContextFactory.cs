using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using UrbanSpork.DataAccess.DataAccess;

namespace UrbanSpork.API
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UrbanDbContext>
    {
        public UrbanDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<UrbanDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString);

            return new UrbanDbContext(builder.Options);
        }
    }
}
