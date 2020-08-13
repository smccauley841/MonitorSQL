using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitorAPI.Models
{
    public class vwDatabaseFileSizes
    {
        public string Name { get; private set; }
        public int InstanceId { get; private set; }
        public double DataFileUsed { get; private set; }
        public double LogFileUsed { get; private set; }
        public string Collation { get; private set; }
        public string Owner { get; private set; }
        public string RecoveryModel { get; private set; }
        public string Status { get; private set; }
        public int DatabaseId { get; private set; }
    }
}
