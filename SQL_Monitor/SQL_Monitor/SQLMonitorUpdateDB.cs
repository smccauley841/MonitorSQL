using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQL_Monitor
{
    class SQLMonitorUpdateDB
    {

        public static void UpdateBackupHistory(int instanceId, string databaseName, DateTime backupTime, string backupType)
        {

            StringBuilder errorMessages = new StringBuilder();
            string query = @"USE [SQL_MONITOR]

INSERT INTO [dbo].[BackupHistory]
           ([InstanceID]
           ,[Database]
           ,[BackupTime]
           ,[BackupType]
           ,[Timestamp])
     VALUES
           (@instanceid,@database,@backuptime,@backuptype, getdate())

";

            SqlConnection conn = new SqlConnection("Data Source=DDWS0655;Initial Catalog=SQL_Monitor;Integrated Security=SSPI");
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@instanceid", instanceId);
            command.Parameters.AddWithValue("@database", databaseName);
            command.Parameters.AddWithValue("@backuptime", backupTime);
            command.Parameters.AddWithValue("@backuptype", backupType);
            try
            {
                command.ExecuteNonQuery();
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
                Console.WriteLine(errorMessages.ToString());

            }

            catch (InvalidOperationException ex)
            {
                Console.WriteLine("DDWS0655: " + ex);
            }

            conn.Close();
        }



        public static void UpdateDBFileSizes(int instanceId, int databaseId, Int64 TotalDataUsedBytes, Int64 TotalLogUsedBytes, Int64 TotalDataSizeBytes, Int64 TotalLogSizeBytes)
        {

            StringBuilder errorMessages = new StringBuilder();
            string query = @"USE [SQL_MONITOR]

INSERT INTO [dbo].[DatabaseFileSize]
           ([InstanceID]
           ,[DatabaseID]
           ,[TotalDataUsedBytes]
           ,[TotalLogUsedBytes]
           ,[TotalDataSizeBytes]
           ,[TotalLogSizeBytes]
           ,[Timestamp])
     VALUES
           (@instanceid,@databaseId,@TotalDataUsedBytes,@TotalLogUsedBytes,@TotalDataSizeBytes,@TotalLogSizeBytes, getdate())

";

            SqlConnection conn = new SqlConnection("Data Source=DDWS0655;Initial Catalog=SQL_Monitor;Integrated Security=SSPI");
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@instanceid", instanceId);
            command.Parameters.AddWithValue("@databaseId", databaseId);
            command.Parameters.AddWithValue("@TotalDataUsedBytes", TotalDataUsedBytes);
            command.Parameters.AddWithValue("@TotalLogUsedBytes", TotalLogUsedBytes);
            command.Parameters.AddWithValue("@TotalDataSizeBytes", TotalDataSizeBytes);
            command.Parameters.AddWithValue("@TotalLogSizeBytes", TotalLogSizeBytes);
            try
            {
                command.ExecuteNonQuery();
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
                Console.WriteLine(errorMessages.ToString());

            }

            catch (InvalidOperationException ex)
            {
                Console.WriteLine("DDWS0655: " + ex);
            }

            conn.Close();
        }

        public static void UpdateServerStats(string physicalServerName, double cpu, double ram, double diskio)
        {

            StringBuilder errorMessages = new StringBuilder();
            string query = @"USE [SQL_MONITOR]

INSERT INTO [dbo].[ServerStats]
           ([PhysicalServerName]
           ,[Timestamp]
           ,[CPU]
           ,[RAM]
           ,[DiskIO])
     VALUES
           (@physicalServerName,getdate(),@cpu,@ram,@diskio)

";

            SqlConnection conn = new SqlConnection("Data Source=DDWS0655;Initial Catalog=SQL_Monitor;Integrated Security=SSPI");
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@physicalServerName", physicalServerName);
            command.Parameters.AddWithValue("@cpu", cpu);
            command.Parameters.AddWithValue("@ram", ram);
            command.Parameters.AddWithValue("@diskio", diskio);
            try
            {
                command.ExecuteNonQuery();
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
                Console.WriteLine(errorMessages.ToString());

            }

            catch (InvalidOperationException ex)
            {
                Console.WriteLine("DDWS0655: " + ex);
            }

            conn.Close();
        }

        public static void UpdateInstancestats(int instanceId, long ple)
        {

            StringBuilder errorMessages = new StringBuilder();
            string query = @"USE [SQL_MONITOR]

INSERT INTO [dbo].[InstanceStats]
           ([PLE]
           ,[Timestamp]
           ,[InstanceID])
     VALUES
           (@ple,getdate(),@instanceid)

";

            SqlConnection conn = new SqlConnection("Data Source=DDWS0655;Initial Catalog=SQL_Monitor;Integrated Security=SSPI");
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@instanceid", instanceId);
            command.Parameters.AddWithValue("@ple", ple);
            try
            {
                command.ExecuteNonQuery();
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
                Console.WriteLine(errorMessages.ToString());

            }

            catch (InvalidOperationException ex)
            {
                Console.WriteLine("DDWS0655: " + ex);
            }
            conn.Dispose();
        }

        public static void UpdateWaitstats(string waitType, decimal waitPercentage, int instanceId)
        {

            StringBuilder errorMessages = new StringBuilder();
            string query = @"USE [SQL_MONITOR]

INSERT INTO [dbo].[InstanceWaitStats]
           ([WaitType]
           ,[WaitPercentage]
           ,[TimeStamp]
           ,[InstanceID])
     VALUES
           (@waittype,@waitpercentage,getdate(),@instanceid)

";

            SqlConnection conn = new SqlConnection("Data Source=DDWS0655;Initial Catalog=SQL_Monitor;Integrated Security=SSPI");
            conn.Open();
            var command = conn.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@instanceid", instanceId);
            command.Parameters.AddWithValue("@waittype", waitType);
            command.Parameters.AddWithValue("@waitpercentage", waitPercentage);
            try
            {
                command.ExecuteNonQuery();
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
                Console.WriteLine(errorMessages.ToString());

            }

            catch (InvalidOperationException ex)
            {
                Console.WriteLine("DDWS0655: " + ex);
            }
            conn.Dispose();
        }
    }
}
