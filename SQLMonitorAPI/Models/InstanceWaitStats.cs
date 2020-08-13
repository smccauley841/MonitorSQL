using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitorAPI.Models
{
    public class InstanceWaitStats
    {
        public int ID { get; set; }
        public string WaitType { get; set; }
        public decimal WaitPercentage { get; set; }
        public DateTime TimeStamp { get; set; }
        public SQLInstance Instance { get; set; }
    }
}
