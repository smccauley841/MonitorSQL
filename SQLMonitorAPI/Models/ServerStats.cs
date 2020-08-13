using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitorAPI.Models
{
    public class ServerStats
    {
        public int ID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string PhysicalServerName { get; set; }
        public double CPU { get; set; }
        public double RAM  { get; set; }
        public double DiskIO { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
