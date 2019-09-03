using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;

namespace Tester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            var services = new ServiceCollection();
            services.AddLogging(logging =>
            {
                logging.AddConsole();
            });
            var sp = services.BuildServiceProvider();
            var builder = new ConfigurationBuilder();
            builder.Properties["LoggerFactory"] = sp.GetService<ILoggerFactory>();
            builder.AddZooKeeper("10.211.55.2:2181", "/config", "digest", "test:test123");

            IConfigurationRoot config = builder.Build();

            Console.WriteLine(config["foo"]);
            Console.ReadLine();
        }
    }
}
