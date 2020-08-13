using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitorAPI.Models
{
    public class InstanceStats
    {
        public int Id { get; set; }
        public long PLE { get; set; }
        public DateTime Timestamp { get; set; }
        public SQLInstance Instance { get; set; }

    }
}
