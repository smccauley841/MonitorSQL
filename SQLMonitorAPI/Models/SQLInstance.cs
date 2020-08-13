using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SQLMonitorAPI.Models
{
    public class SQLInstance
    {
        public int ID { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string PhysicalServerName { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string SQLVersion { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string SQLCollation { get; set; }
        [Required]
        public int NoOfDatabases { get; set; }
        public DateTime SQLCreateDate { get; set; }
        public int IsClustered { get; set; }
        public int IsAlwaysOn { get; set; }
        public string OSName { get; set; }
        public string SQLEdition { get; set; }

        public ICollection<SQLDatabases> SQLDatabases { get; set; }
        public ICollection<BackupHistory> BackupHistory { get; set; }
        public ICollection<DatabaseFileSize> DatabaseFileSize { get; set; }
    }
}