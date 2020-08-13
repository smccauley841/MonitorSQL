using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitorAPI.Models
{
    public class DatabaseFileSize
    {
        public int ID { get; set; }
        public SQLInstance Instance { get; set; }
        public SQLDatabases Database { get; set; }
        public Int64 TotalDataUsedBytes { get; set; }
        public Int64 TotalLogUsedBytes { get; set; }
        public Int64 TotalDataSizeBytes { get; set; }
        public Int64 TotalLogSizeBytes { get; set; }
        public DateTime Timestamp { get; set; }


    }
}
