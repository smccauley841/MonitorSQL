using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitorAPI.Models
{
    public class BackupHistory
    {
        public int Id { get; set; }
        [Required]
        public string Database { get; set; }
        public DateTime BackupTime { get; set; }
        [StringLength(1)]
        [Required]
        public string BackupType { get; set; }
        public DateTime Timestamp { get; set; }
        public SQLInstance Instance { get; set; }

        
    }
}
