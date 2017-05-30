using System;
using System.Collections.Generic;
using System.Linq;
using AutomationFramework.UITests.Framework.Base;
using Microsoft.Extensions.Configuration;

namespace AutomationFramework.UITests.Framework.Config
{
    public class Settings
    {
        public Settings(IConfigurationRoot configuration)
        {
            RunSettings = new RunSettings(configuration);
            var accountsSection = configuration.GetSection("Accounts");
            Accounts = ParseAccounts(accountsSection);
        }

        public RunSettings RunSettings { get; }

        public Dictionary<string, Account> Accounts { get; }

        private Dictionary<string, Account> ParseAccounts(IConfiguration accountsSection)
        {
            var children = accountsSection.GetChildren();
            return children.ToDictionary(subSection => subSection.Key, subSection => new Account(subSection));
        }
    }

    public class RunSettings
    {
        private readonly IConfigurationRoot _configuration;

        public RunSettings(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public string FrontAdminUrl => _configuration[typeof(RunSettings).Name + ":FrontAdminUrl"];

        public string FrontWebUrl => _configuration[typeof(RunSettings).Name + ":FrontWebUrl"];

        public string TestType => _configuration[typeof(RunSettings).Name + ":TestType"];

        public BrowserType BrowserType
            => (BrowserType) Enum.Parse(typeof(BrowserType), _configuration[typeof(RunSettings).Name + ":BrowserType"]);

        public bool IsLog => Convert.ToBoolean(_configuration[typeof(RunSettings).Name + ":IsLog"]);

        public string ChromeDriverPath => _configuration[typeof(RunSettings).Name + ":ChromeDriverPath"];

        public string PhantomDriverPath => _configuration[typeof(RunSettings).Name + ":PhantomDriverPath"];

        public string ScreenshotsPath => _configuration[typeof(RunSettings).Name + ":ScreenshotsPath"];

        public string ExcelDataPath => _configuration[typeof(RunSettings).Name + ":ExcelDataPath"];

        public string Company => _configuration[typeof(RunSettings).Name + ":Company"];

        public string MicrosoftEmail => _configuration[typeof(RunSettings).Name + ":MicrosoftEmail"];

        public string MicrosoftPassword => _configuration[typeof(RunSettings).Name + ":MicrosoftPassword"];
    }

    public class Account
    {
        private readonly IConfigurationSection _configuration;

        public Account(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        public string Username => _configuration[typeof(Account).Name + ":Username"];

        public string Password => _configuration["Password"];

        public string Company => _configuration["Company"];
    }
}