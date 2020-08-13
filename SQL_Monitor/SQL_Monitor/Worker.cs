using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SQL_Monitor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public SqlConnection sqlCon;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            sqlCon = new SqlConnection("Data Source=DDWS0655;Initial Catalog=SQL_Monitor;Integrated Security=SSPI");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("SQL Full Backup running at: {time}", DateTimeOffset.Now);

                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                TimeSpan scheduleTime = new TimeSpan(10, 23, 0);
                if (currentTime == scheduleTime)
                {
                    SQLMonitorConnection();
                }

                await Task.Delay(60000, stoppingToken);
            }
        }

        private static void SQLMonitorConnection()
        {
            using (var sqlmonitor = new SqlConnection("Data Source=DDWS0655;Initial Catalog=SQL_Monitor;Integrated Security=SSPI"))
            {
                var sqlinstances = new DataSet();
                string query = "select sqlinstance from tbl_sqlserverlist_test";
                SqlDataAdapter SQLInstanceDA = new SqlDataAdapter(query, sqlmonitor);
                SQLInstanceDA.Fill(sqlinstances);
                foreach (DataTable table in sqlinstances.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        SQLBackupQueries.SqlFullBackupLastRun(row["sqlinstance"].ToString());
        
                    }
                }
                SQLInstanceDA.Dispose();
                sqlmonitor.Close();
            }
        }

        
    }
}