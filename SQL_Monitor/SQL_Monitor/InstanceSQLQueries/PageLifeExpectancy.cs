using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InstanceSQLQueries
{
    class PageLifeExpectancy
    {
        public static long GetPageLifeExpectancy(string serverName)
        {

            StringBuilder errorMessages = new StringBuilder();
            long pleResult = 0;

            var sqlcon = "Data Source = " + serverName + "; Initial Catalog = master; Integrated Security = SSPI";
            using (var sqlmon = new SqlConnection(sqlcon))
            {
                var ple = new DataSet();
                string query = @"select cntr_value as [PageLifeExpectancy]
from sys.dm_os_performance_counters with (nolock)
where object_name like N'%Buffer Manager%'
and counter_name = N'Page life expectancy' option (recompile)";

                try
                {
                    var SQLInstanceDA = new SqlDataAdapter(query, sqlmon);
                    SQLInstanceDA.Fill(ple);
                    foreach (DataTable pletable in ple.Tables)
                    {
                        foreach (DataRow plerow in pletable.Rows)
                        {
                            pleResult = (long)plerow["PageLifeExpectancy"];
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
                    Console.WriteLine(serverName + ": " + ex);
                }

                ple.Dispose();
            }

            return pleResult;
        }
    }
}
