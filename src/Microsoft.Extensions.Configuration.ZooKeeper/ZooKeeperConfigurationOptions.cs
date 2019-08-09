using System;

namespace Microsoft.Extensions.Configuration
{
    public class ZooKeeperConfigurationOptions
    {
        public ZooKeeperConfigurationOptions(
            string connectionString, 
            string path,
            string? schema,
            string? auth)
        {
            ConnectionString = connectionString;
            Path = path;
            Schema = schema;
            Auth = auth;
        }

        public string ConnectionString { get; }
        public string Path { get; }
        public string? Schema { get; }
        public string? Auth { get; }
    }
}
