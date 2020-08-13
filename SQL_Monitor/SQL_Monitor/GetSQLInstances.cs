using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;

namespace SQL_Monitor
{
    public class GetSQLInstances
    {
        static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        public static DataSet SQLInstances()
        {          

            var connectionString = configuration.GetSection("DefaultConnection").Value;
            using (SqlConnection sqlmonitor = new SqlConnection(connectionString))
            {
                var sqlinstances = new DataSet();
                string query = @"SELECT [ID]
      ,[Name]
      ,[SQLVersion]
  FROM [SQL_MONITOR].[dbo].[SQLInstances]";
                SqlDataAdapter SQLInstanceDA = new SqlDataAdapter(query, sqlmonitor);
                SQLInstanceDA.Fill(sqlinstances);
                SQLInstanceDA.Dispose();
                return sqlinstances;
                
            }
        }

        public static DataSet GetSQLDatabases()
        {
            var connectionString = configuration.GetSection("DefaultConnection").Value;
            using (SqlConnection sqlmonitor = new SqlConnection(connectionString))
            {
                var sqldatabases = new DataSet();
                string query = @"  select i.id as [instanceid], i.name as [sqlinstance], d.id as [databaseid], d.name as [database] from SQL_MONITOR.dbo.sqldatabases d
  join sql_monitor.dbo.sqlinstances i on i.id = d.instanceid
where d.status = 'online'";
                SqlDataAdapter SQLDatabaseDA = new SqlDataAdapter(query, sqlmonitor);
                SQLDatabaseDA.Fill(sqldatabases);
                SQLDatabaseDA.Dispose();
                return sqldatabases;

            }
        }

        public static DataSet GetPhysicalMachines()
        {
            var connectionString = configuration.GetSection("DefaultConnection").Value;
            using (SqlConnection sqlmonitor = new SqlConnection(connectionString))
            {
                var physicalMachines = new DataSet();
                string query = @"select distinct physicalServerName from SQLInstances";
                SqlDataAdapter physicalMachinesDA = new SqlDataAdapter(query, sqlmonitor);
                physicalMachinesDA.Fill(physicalMachines);
                physicalMachinesDA.Dispose();
                return physicalMachines;

            }
        }
    }
}
