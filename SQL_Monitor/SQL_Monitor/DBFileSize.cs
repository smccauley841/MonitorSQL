using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQL_Monitor
{
    class DBFileSize : BackgroundService
    {
        private readonly ILogger<DBFileSize> _logger;

        public DBFileSize(ILogger<DBFileSize> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("SQL FileSize Starting: {time}", DateTimeOffset.Now);
                SqlDbFileSize();
                _logger.LogInformation("SQL FileSize Ending : {time}", DateTimeOffset.Now);
                await Task.Delay( 24 * 60 * 60000, stoppingToken);
            }
        }

        public static void SqlDbFileSize()
        {

            DataSet sqlinstances = GetSQLInstances.GetSQLDatabases();
            StringBuilder errorMessages = new StringBuilder();
            foreach (DataTable table in sqlinstances.Tables)
            {
                foreach (DataRow row in table.Rows)
                {

                    
                    var sqlcon = "Data Source = " + row["sqlinstance"].ToString() + "; Initial Catalog = " + row["database"] + "; Integrated Security = SSPI";
                    using (var sqlmon = new SqlConnection(sqlcon))
                    {
                        var databaseFileSize = new DataSet();
                        string query = @"SELECT
                            SUM(CONVERT(BIGINT,CASE WHEN df.type = 0 THEN CAST(FILEPROPERTY(df.[name], 'SpaceUsed') AS bigint) * 8 * 1024 ELSE 0 end)) [TotalDataUsedBytes],
                            SUM(CONVERT(BIGINT,CASE WHEN df.type = 1 THEN CAST(FILEPROPERTY(df.[name], 'SpaceUsed') AS bigint) * 8 * 1024 ELSE 0 end)) [TotalLogUsedBytes],
							SUM(CONVERT(BIGINT,CASE WHEN df.type = 0 THEN CAST(df.size AS bigint) * 8 * 1024 ELSE 0 end)) [TotalDataSizeBytes],
							SUM(CONVERT(BIGINT,CASE WHEN df.type = 1 THEN CAST(df.size AS bigint) * 8 * 1024 ELSE 0 end)) [TotalLogSizeBytes]
                         FROM
						 sys.database_files df;";
                        try
                        {
                            var SQLInstanceDA = new SqlDataAdapter(query, sqlmon);
                            SQLInstanceDA.Fill(databaseFileSize);
                            foreach (DataTable fileSizeTable in databaseFileSize.Tables)
                            {
                                foreach (DataRow fileSizeRow in fileSizeTable.Rows)
                                {
                                    SQLMonitorUpdateDB.UpdateDBFileSizes((int)row["instanceid"], (int)row["databaseid"] , (Int64)fileSizeRow["TotalDataUsedBytes"],
                                        (Int64)fileSizeRow["TotalLogUsedBytes"], (Int64)fileSizeRow["TotalDataSizeBytes"], (Int64)fileSizeRow["TotalDataSizeBytes"]);
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
                            Console.WriteLine(row["sqlinstance"].ToString() + " " + row["database"].ToString() + " "  + errorMessages);
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        databaseFileSize.Dispose();
                    }
                }
            }
        }
    }
}
