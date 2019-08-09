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
        private readonly string _connectionString;
        private readonly string _path;
        private Watcher watcher;
        private const int ZOOKEEPER_CONNECTION_TIMEOUT = 2000;

        public ZooKeeperConfigurationProvider(string connectionString, string path)
        {
            this._connectionString = connectionString;
            this._path = path;
        }

        public override void Load()
        {
            LoadAsync().GetAwaiter().GetResult();
        }

        private async Task LoadAsync()
        {
            await UsingZookeeper(_connectionString, async zk =>
            {
                IEnumerable<(string key, string value)> allData = await GetData(zk, "");
                Data = allData.ToDictionary(kvp => kvp.key, kvp => kvp.value);
            });
        }

        private async Task<IEnumerable<(string key, string value)>> GetData(ZooKeeper zk, string key)
        {
            string aPath = _path + '/' + key;
            ChildrenResult childrenResult = await zk.getChildrenAsync(key);
            if (childrenResult.Children.Count == 0)
            {
                DataResult dataResult = await zk.getDataAsync(key);
                string value = Encoding.UTF8.GetString(dataResult.Data);
                return new[] { (key.Replace('/', ':'), value) };
            }
            else
            {
                IEnumerable<Task<IEnumerable<(string key, string value)>>> getAllChildrenTask = childrenResult.Children
                    .Select(child => GetData(zk, key + '/' + child));
                IEnumerable<(string key, string value)>[] result = await Task.WhenAll(getAllChildrenTask);
                return result.SelectMany(r => r);
            }
        }

        private static Task<T> UsingZookeeper<T>(Func<ZooKeeper, Task<T>> zkMethod, string deploymentConnectionString, Watcher watcher, bool canBeReadOnly = false)
        {
            return ZooKeeper.Using(deploymentConnectionString, ZOOKEEPER_CONNECTION_TIMEOUT, null, zkMethod, canBeReadOnly);
        }

        private Task UsingZookeeper(string connectString, Func<ZooKeeper, Task> zkMethod)
        {
            return ZooKeeper.Using(connectString, ZOOKEEPER_CONNECTION_TIMEOUT, null, zkMethod);
        }
    }
}