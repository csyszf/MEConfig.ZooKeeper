using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Microsoft.Extensions.Configuration
{
    internal class ZooKeeperConfigurationSource : IConfigurationSource
    {
        private readonly ZooKeeperConfigurationOptions _options;

        public ZooKeeperConfigurationSource(ZooKeeperConfigurationOptions options)
        {
            _options = options;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            ILoggerFactory loggerFactory = NullLoggerFactory.Instance;
            if (builder.Properties.ContainsKey("LoggerFactory"))
            {
                loggerFactory = (ILoggerFactory)builder.Properties["LoggerFactory"];
            }
            return new ZooKeeperConfigurationProvider(_options, loggerFactory.CreateLogger<ZooKeeperConfigurationProvider>());
        }
    }
}