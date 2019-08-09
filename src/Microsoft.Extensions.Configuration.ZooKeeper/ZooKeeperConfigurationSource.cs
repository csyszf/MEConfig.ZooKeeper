namespace Microsoft.Extensions.Configuration
{
    internal class ZooKeeperConfigurationSource : IConfigurationSource
    {
        private readonly ZooKeeperConfigurationOptions options;

        public ZooKeeperConfigurationSource(ZooKeeperConfigurationOptions options)
        {
            this.options = options;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ZooKeeperConfigurationProvider(options);
        }
    }
}