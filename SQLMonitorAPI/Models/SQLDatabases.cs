using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SQLMonitorAPI.Models
{
    public class SQLDatabases
    {
        public int ID { get; set; }      
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Collation { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Status { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string RecoveryModel { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Owner { get; set; }
        public SQLInstance Instance { get; set; }

        public ICollection<DatabaseFileSize> DatabaseFileSize { get; set; }
    }
}
