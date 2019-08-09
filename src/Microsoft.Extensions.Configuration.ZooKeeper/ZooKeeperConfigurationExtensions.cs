using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Configuration
{
    public static class ZooKeeperConfigurationExtensions
    {
        /// <summary>
        /// Adds an <see cref="IConfigurationProvider"/> that reads configuration values from the ZooKeeper.
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="connectionString">ZooKeeper connection string.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddZooKeeper(
            this IConfigurationBuilder configurationBuilder,
            string connectionString)
        {
            return AddZooKeeper(configurationBuilder, connectionString, "/");
        }

        /// <summary>
        /// Adds an <see cref="IConfigurationProvider"/> that reads configuration values from the ZooKeeper.
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="connectionString">ZooKeeper connection string.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddZooKeeper(
            this IConfigurationBuilder configurationBuilder,
            string connectionString,
            string configPath)
        {
            return AddZooKeeper(configurationBuilder, new ZooKeeperConfigurationOptions(connectionString, configPath, null, null));
        }


        /// <summary>
        /// Adds an <see cref="IConfigurationProvider"/> that reads configuration values from the ZooKeeper.
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="connectionString">ZooKeeper connection string.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddZooKeeper(
            this IConfigurationBuilder configurationBuilder,
            string connectionString,
            string configPath,
            string? schema,
            string? auth)
        {
            return AddZooKeeper(configurationBuilder, new ZooKeeperConfigurationOptions(connectionString, configPath, schema, auth));
        }

        /// <summary>
        /// Adds an <see cref="IConfigurationProvider"/> that reads configuration values from the ZooKeeper.
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="options">The <see cref="ZooKeeperConfigurationOptions"/> to use.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddZooKeeper(this IConfigurationBuilder configurationBuilder, ZooKeeperConfigurationOptions options)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (options.ConnectionString == string.Empty)
            {
                throw new ArgumentNullException(nameof(options.ConnectionString));
            }

            configurationBuilder.Add(new ZooKeeperConfigurationSource(options));

            return configurationBuilder;
        }
    }
}
