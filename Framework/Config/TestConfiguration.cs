using System;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace AutomationFramework.UITests.Framework.Config
{
    [CollectionDefinition("TestConfigurationCollection")]
    public class TestConfigurationCollection : ICollectionFixture<TestConfiguration>
    {
    }

    public class TestConfiguration : IDisposable
    {
        public TestConfiguration()
        {
            var configuration = new ConfigurationBuilder()
#if DEBUG
                .AddJsonFile(@"Config\GlobalConfig.Local.Dev.json", false, true)
#else
                .AddJsonFile(@"Config\GlobalConfig.Deploy.json", optional: false, reloadOnChange: true)
#endif
                .Build();
            Settings = new Settings(configuration);
        }

        public Settings Settings { get; }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }
}