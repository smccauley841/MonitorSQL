using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SQL_Monitor
{
    public class SQLMonitorConnection
    {
        public static readonly SqlConnection sqlcon = new SqlConnection("Data Source=DDWS0655;Initial Catalog=SQL_Monitor;Integrated Security=SSPI");
    }
}
