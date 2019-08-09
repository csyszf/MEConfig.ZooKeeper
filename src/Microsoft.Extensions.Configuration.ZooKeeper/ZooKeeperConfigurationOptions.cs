using System;

namespace Microsoft.Extensions.Configuration
{
    public class ZooKeeperConfigurationOptions
    {
        public ZooKeeperConfigurationOptions(string connectionString, string path)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public string ConnectionString { get; set; } = default!;
        public string Path { get; set; } = default!;
    }
}
