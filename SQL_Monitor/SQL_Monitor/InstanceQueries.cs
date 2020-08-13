using InstanceSQLQueries;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQL_Monitor
{
    class InstanceQueries : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                GetInstanceStats();
                await Task.Delay(60000, stoppingToken);
            }
        }

        public static void GetInstanceStats()
        {
            DataSet sqlinstances = GetSQLInstances.SQLInstances();
            foreach (DataTable table in sqlinstances.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    long ple = PageLifeExpectancy.GetPageLifeExpectancy(row["name"].ToString());
                    WaitType.GetWaitStats(row["name"].ToString(), (int)row["Id"]);
                    SQLMonitorUpdateDB.UpdateInstancestats((int)row["id"], ple);
                }
            }
        }


       
    }
}
