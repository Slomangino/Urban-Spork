using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.Domain.ReadModel.ConnectionFactory
{
    public class ConnectionFactory
    {
        public NpgsqlConnection GetUserConnection()
        {
            var connection = new Npgsql.NpgsqlConnection("Host=urbansporkdb.cj0fybtxusp9.us-east-1.rds.amazonaws.com;Port=5405;User Id=yamnel;Password=urbansporkpass;Database=urbansporkdb");
            return connection;
        }
    }
}
