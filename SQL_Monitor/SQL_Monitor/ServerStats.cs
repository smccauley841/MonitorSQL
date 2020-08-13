using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SQL_Monitor
{
    class ServerStats : BackgroundService
    {
        private static PerformanceCounter cpuCounter;
        private static PerformanceCounter ramCounter;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                GetMachineStats();

                await Task.Delay(60000, stoppingToken);
            }
        }

        public static void GetMachineStats()
        {
            DataSet servers = GetSQLInstances.GetPhysicalMachines();
            foreach (DataTable table in servers.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    double cpu = GetMachineCPU(row["physicalServerName"].ToString());
                    double ram = GetMachineRAM(row["physicalServerName"].ToString());
                    double diskIO = GetDiskReads(row["physicalServerName"].ToString());
                    SQLMonitorUpdateDB.UpdateServerStats(row["physicalServerName"].ToString(), cpu, ram, diskIO);
                }


            }
        }

        public static double GetMachineCPU(string serverName)
        {
            var cpuValue = 0.0;
            try
            {
                cpuCounter = new PerformanceCounter
                {
                    MachineName = serverName,
                    CategoryName = "Processor",
                    CounterName = "% Processor Time",
                    InstanceName = "_Total"
                };

                cpuCounter.NextValue();
                cpuValue = Math.Round(cpuCounter.NextValue(), 2);
            }

            catch (System.ComponentModel.Win32Exception)
            {
                Console.WriteLine("Unable to connect to " + serverName);
            }
            cpuCounter.Close();
            return cpuValue;
        }

        public static double GetMachineRAM(string serverName)
        {
            var ramValue = 0.0;
            try
            {

                ramCounter = new PerformanceCounter
                {
                    MachineName = serverName,
                    CategoryName = "Memory",
                    CounterName = "% Committed Bytes in Use"
                };

                ramCounter.NextValue();
                ramValue = Math.Round(ramCounter.NextValue(), 0);
            }

            catch (System.ComponentModel.Win32Exception)
            {
                Console.WriteLine("Unable to connect to " + serverName);
            }
            ramCounter.Close();
            return ramValue;

        }

        public static double GetDiskReads(string serverName)
        {
            var totalDiskReadWrite = 0.0;
            try
            {

                PerformanceCounter diskReadsPerSec = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total", serverName);
                PerformanceCounter diskWritesPerSec = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total", serverName);

                diskReadsPerSec.NextValue();
                diskWritesPerSec.NextValue();

                var diskReads = Math.Round(diskReadsPerSec.NextValue());
                var diskWrites = Math.Round(diskWritesPerSec.NextValue());
                //ramValue = Math.Round(ramCounter.NextValue(), 0);

                totalDiskReadWrite = Math.Round(((diskReads + diskWrites) / 1024f) / 1024f);
                
                diskReadsPerSec.Close();
                diskWritesPerSec.Close();
            }

            catch (System.ComponentModel.Win32Exception)
            {
                Console.WriteLine(serverName + " error");
            }
            
            return totalDiskReadWrite;

        }

        
    }
}


