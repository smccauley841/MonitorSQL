using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SQL_Monitor
{
    class SQLLogBackup : BackgroundService
    {

        private readonly ILogger<SQLLogBackup> _logger;

        public SQLLogBackup(ILogger<SQLLogBackup> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("SQL Log Backup running at: {time}", DateTimeOffset.Now);

                SqlLogBackupLastRun();

                await Task.Delay(60 * 60000, stoppingToken);
            }
        }
               
        public static void SqlLogBackupLastRun()
        {

            DataSet sqlinstances = GetSQLInstances.SQLInstances();
            StringBuilder errorMessages = new StringBuilder();
            foreach (DataTable table in sqlinstances.Tables)
            {
                foreach (DataRow row in table.Rows)
                {

                    var insid = (int)row["id"];
                    var sqlcon = "Data Source = " + row["name"].ToString() + "; Initial Catalog = master; Integrated Security = SSPI";
                    using (var sqlmon = new SqlConnection(sqlcon))
                    {
                        var logBackupDs = new DataSet();
                        string query = @"WITH cte AS (
    SELECT
        bs.database_name,
        bs.type,
        bs.backup_size,
        bs.backup_start_date,
        bs.backup_finish_date,
        bms.is_password_protected,
        bmf.physical_device_name,
        CASE
            WHEN bmf.device_type = 2 THEN 1
            ELSE 0
        END AS is_native_backup,
        bmf.logical_device_name,
        ROW_NUMBER() OVER (
            PARTITION BY bs.database_name,
            bs.type
            ORDER BY
                bs.backup_finish_date DESC
        ) [order_id]
    FROM
        msdb.dbo.backupset bs
        INNER JOIN msdb.dbo.backupmediaset bms ON bms.media_set_id = bs.media_set_id
        INNER JOIN msdb.dbo.backupmediafamily bmf ON bmf.media_set_id = bs.media_set_id
    WHERE
        DB_ID(bs.database_name) IS NOT NULL
)
SELECT
    database_name,
    backup_finish_date
FROM
    cte
WHERE
    cte.order_id = 1 --and type = 'D'
    and backup_finish_date = (
        SELECT
            MAX(backup_finish_date)
        FROM
            cte cte2
        WHERE
            cte.database_name = cte2.database_name
            and type = 'L'
    )
ORDER BY
    database_name";
                        try
                        {
                            var SQLInstanceDA = new SqlDataAdapter(query, sqlmon);
                            SQLInstanceDA.Fill(logBackupDs);
                            foreach (DataTable logBKtable in logBackupDs.Tables)
                            {
                                foreach (DataRow logBKrow in logBKtable.Rows)
                                {                                    
                                    SQLMonitorUpdateDB.UpdateBackupHistory((int)row["id"], logBKrow["database_name"].ToString(), (DateTime)logBKrow["backup_finish_date"], "L");
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
                            Console.WriteLine(errorMessages);
                        }

                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        logBackupDs.Dispose();
                    }
                }
            }
        }
    }
}
