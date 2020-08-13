using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitorAPI.Models
{
    public class vwInstanceWaitStats
    {
        public int InstanceId { get; set; }
        public string WaitType { get; set; }
        public decimal WaitPercentage { get; set; }
        public DateTime timestamp { get; set; }
    }
}
