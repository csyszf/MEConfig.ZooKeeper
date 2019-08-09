using Microsoft.Extensions.Configuration;
using System;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        static void Run()
        {
            var builder = new ConfigurationBuilder();
            builder.AddZooKeeper("10.211.55.2:2181", "/config", "digest", "test:test123");

            var config = builder.Build();

            Console.WriteLine(config["foo"]);
        }
    }
}
