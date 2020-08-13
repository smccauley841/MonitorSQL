using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace SQL_Monitor
{
    public class SQLBackupQueries
    {
        public static void SqlFullBackupLastRun(string sqlInstance)
        {
            StringBuilder errorMessages = new StringBuilder();
            var sqlcon = "Data Source = " + sqlInstance + "; Initial Catalog = master; Integrated Security = SSPI";
            using SqlConnection sqlmon = new SqlConnection(sqlcon);
            var sqlinstances = new DataSet();
            string query = @"WITH cte AS (
    SELECT
        bs.database_name,
        bs.type,
        bs.backup_size,
        bs.backup_start_date,
        bs.backup_finish_date,
        bs.is_copy_only,
        bms.is_compressed,
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
            and type = 'D'
    )
ORDER BY
    database_name";
            try
            {
                using (SqlDataAdapter SQLInstanceDA = new SqlDataAdapter(query, sqlmon))
                {
                    SQLInstanceDA.Fill(sqlinstances);
                }
                foreach (DataTable table in sqlinstances.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        using StreamWriter file = new StreamWriter(@"C:\Temp\FullBackups.txt", true);
                        file.WriteLine(sqlInstance + " " + row["database_name"].ToString() + " " + row["backup_finish_date"].ToString());

                    }
                }
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
            }

            catch (InvalidOperationException ex)
            {
                Console.WriteLine(sqlInstance + ": " + ex);
            }
        }
    }
}
