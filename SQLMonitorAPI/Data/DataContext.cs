using Microsoft.EntityFrameworkCore;
using SQLMonitorAPI.Models;

namespace SQLMonitorAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<SQLInstance> SQLInstances { get; set; }
        public DbSet<BackupHistory> BackupHistory { get; set; }
        public DbSet<SQLDatabases> SQLDatabases { get; set; }
        public DbSet<DatabaseFileSize> DatabaseFileSize { get; set; }
        public DbSet<ServerStats> ServerStats { get; set; }
        public DbSet<vwDatabaseFileSizes> vwDatabaseFileSizes { get; set; }
        public DbSet<InstanceStats> InstanceStats { get; set; }
        public DbSet<InstanceWaitStats> InstanceWaitStats { get; set; }
        public DbSet<vwInstanceWaitStats> vwInstanceWaitStats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<vwDatabaseFileSizes>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("DatabaseFileSizes");
                    eb.Property(v => v.Name).HasColumnName("Name");
                })
                .Entity< vwInstanceWaitStats>(eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("vwInstanceWaitStats");
                    eb.Property(v => v.InstanceId).HasColumnName("InstanceId");
                });
        }

    }
}