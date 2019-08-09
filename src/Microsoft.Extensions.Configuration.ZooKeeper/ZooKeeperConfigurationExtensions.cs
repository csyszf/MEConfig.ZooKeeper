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
        /// <param name="vault">The ZooKeeper uri.</param>
        /// <param name="clientId">The application client id.</param>
        /// <param name="clientSecret">The client secret to use for authentication.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddZooKeeper(
            this IConfigurationBuilder configurationBuilder,
            string vault,
            string clientId,
            string clientSecret)
        {
            throw new NotImplementedException();
            //return AddZooKeeper(configurationBuilder, new ZooKeeperConfigurationOptions(vault, clientId, clientSecret));
        }

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
            return AddZooKeeper(configurationBuilder, new ZooKeeperConfigurationOptions(connectionString, string.Empty));
        }

        ///// <summary>
        ///// Adds an <see cref="IConfigurationProvider"/> that reads configuration values from the ZooKeeper.
        ///// </summary>
        ///// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        ///// <param name="vault">ZooKeeper uri.</param>
        ///// <param name="manager">The <see cref="IKeyVaultSecretManager"/> instance used to control secret loading.</param>
        ///// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        //public static IConfigurationBuilder AddZooKeeper(
        //    this IConfigurationBuilder configurationBuilder,
        //    string vault,
        //    IKeyVaultSecretManager manager)
        //{
        //    return AddZooKeeper(configurationBuilder, new ZooKeeperConfigurationOptions(vault)
        //    {
        //        Manager = manager
        //    });
        //}

        ///// <summary>
        ///// Adds an <see cref="IConfigurationProvider"/> that reads configuration values from the ZooKeeper.
        ///// </summary>
        ///// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        ///// <param name="vault">ZooKeeper uri.</param>
        ///// <param name="client">The <see cref="KeyVaultClient"/> to use for retrieving values.</param>
        ///// <param name="manager">The <see cref="IKeyVaultSecretManager"/> instance used to control secret loading.</param>
        ///// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        //public static IConfigurationBuilder AddZooKeeper(
        //    this IConfigurationBuilder configurationBuilder,
        //    string vault,
        //    KeyVaultClient client,
        //    IKeyVaultSecretManager manager)
        //{
        //    return configurationBuilder.Add(new ZooKeeperConfigurationSource(new ZooKeeperConfigurationOptions()
        //    {
        //        Client = client,
        //        Vault = vault,
        //        Manager = manager
        //    }));
        //}

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
