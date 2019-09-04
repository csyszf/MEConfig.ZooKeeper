using Microsoft.Extensions.Logging;
using org.apache.zookeeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
    internal class ZooKeeperConfigurationProvider : ConfigurationProvider
    {
        private readonly ZooKeeperConfigurationOptions _options;
        private readonly ZooKeeperWatcher _watcher;
        private const int ZOOKEEPER_CONNECTION_TIMEOUT = 2000;

        public ZooKeeperConfigurationProvider(
            ZooKeeperConfigurationOptions options,
            ILogger<ZooKeeperConfigurationProvider> logger)
        {
            _options = options;
            _watcher = new ZooKeeperWatcher(logger);
        }

        public override void Load()
        {
            LoadAsync().GetAwaiter().GetResult();
        }

        private async Task LoadAsync()
        {
            await UsingZookeeper(_options.ConnectionString, async zk =>
            {
                if (_options.Schema != null)
                {
                    byte[] auth = Encoding.UTF8.GetBytes(_options.Auth);
                    zk.addAuthInfo(_options.Schema, auth);
                }
                IEnumerable<(string key, string value)> allData = await GetData(zk);
                Data = allData.ToDictionary(kvp => kvp.key, kvp => kvp.value);
            });
        }

        private async Task<IEnumerable<(string key, string value)>> GetData(ZooKeeper zk)
        {
            ChildrenResult childrenResult = await zk.getChildrenAsync(_options.Path);
            IEnumerable<Task<IEnumerable<(string key, string value)>>> getAllChildrenTask = childrenResult.Children
                .Select(child => GetData(zk, _options.Path + '/' + child));
            IEnumerable<(string key, string value)>[] result = await Task.WhenAll(getAllChildrenTask);
            return result.SelectMany(r => r);
        }

        private async Task<IEnumerable<(string key, string value)>> GetData(ZooKeeper zk, string key)
        {
            ChildrenResult childrenResult = await zk.getChildrenAsync(key);
            if (childrenResult.Children.Count == 0)
            {
                DataResult dataResult = await zk.getDataAsync(key);
                string value = Encoding.UTF8.GetString(dataResult.Data);
                return new[] { (key.Replace(_options.Path + "/", string.Empty).Replace('/', ':'), value) };
            }
            else
            {
                IEnumerable<Task<IEnumerable<(string key, string value)>>> getAllChildrenTask = childrenResult.Children
                    .Select(child => GetData(zk, key + '/' + child));
                IEnumerable<(string key, string value)>[] result = await Task.WhenAll(getAllChildrenTask);
                return result.SelectMany(r => r);
            }
        }

        private Task UsingZookeeper(string connectString, Func<ZooKeeper, Task> zkMethod)
        {
            return ZooKeeper.Using(connectString, ZOOKEEPER_CONNECTION_TIMEOUT, _watcher, zkMethod);
        }
    }

    internal class ZooKeeperWatcher : Watcher
    {
        private readonly ILogger _logger;
        public ZooKeeperWatcher(ILogger logger)
        {
            _logger = logger;
        }

        public override Task process(WatchedEvent @event)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(@event.ToString());
            }
            return Task.CompletedTask;
        }
    }
}
