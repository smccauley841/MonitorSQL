using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SQL_Monitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton(hostContext.Configuration);
                services.AddHostedService<Worker>();
                services.AddHostedService<SQLLogBackup>();
                services.AddHostedService<DBFileSize>();
                services.AddHostedService<ServerStats>();
                services.AddHostedService<InstanceQueries>();
            });
    }
}
