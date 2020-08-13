using Microsoft.Data.SqlClient;
using SQL_Monitor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InstanceSQLQueries
{
    class WaitType
    {
        public static void GetWaitStats(string serverName, int instanceId)
        {
            StringBuilder errorMessages = new StringBuilder();
            var sqlcon = "Data Source = " + serverName + "; Initial Catalog = master; Integrated Security = SSPI";
            using var sqlmon = new SqlConnection(sqlcon);
            var waitStats = new DataSet();
            string query = @"with [waits]
as (select wait_type, wait_time_ms/1000.0 as [WaitS],
		(wait_time_ms - signal_wait_time_ms) / 1000.0 as [ResourceS],
		signal_wait_time_ms / 1000.0 as [SignalS],
		waiting_tasks_count as [WaitCount],
		100.0 * wait_time_ms / SUM (wait_time_ms) over() as [percentage],
		ROW_NUMBER() over(order by wait_time_ms desc) as [rownum]
	from sys.dm_os_wait_stats with (nolock)
	where [wait_type] not in ('')
	and waiting_tasks_count > 0)
select top 10
	max (w1.wait_type) as [WaitType],
	cast (max (w1.percentage) as decimal (16,2)) as [WaitPercentage]
from waits as w1
inner join waits as w2
on w2.rownum <= w1.rownum
group by w1.rownum
having sum(w2.percentage) - Max(w1.percentage) < 99 --percentage threshold
order by cast (max (w1.percentage) as decimal (16,2)) desc
option (recompile)";

            try
            {
                var SQLInstanceDA = new SqlDataAdapter(query, sqlmon);
                SQLInstanceDA.Fill(waitStats);
                foreach (DataTable wsTable in waitStats.Tables)
                {
                    foreach (DataRow wsRow in wsTable.Rows)
                    {
                        SQLMonitorUpdateDB.UpdateWaitstats(wsRow["WaitType"].ToString(), (decimal)wsRow["WaitPercentage"], instanceId);
                    }
                }

                SQLInstanceDA.Dispose();
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                    "Message: " + ex.Errors[i].Message + "\n" +
                    "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                    "Source: " + ex.Errors[i].Source + "\n" +
                    "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(serverName + ": " + errorMessages);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(serverName + ": " + ex);
            }
        }
    }
}
