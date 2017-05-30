using System;
using System.Drawing;
using AutomationFramework.UITests.Framework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using System.Diagnostics;
using Selenium.WebDriver.Extensions;

namespace AutomationFramework.UITests.Framework.Base
{
    public class Browser
    {
        private readonly string _screenshotsPath;

        public Browser(IWebDriver driver, BrowserType browserType, string screenshotsPath)
        {
            Driver = driver;
            Type = browserType;
            _screenshotsPath = screenshotsPath;
        }

        public IWebDriver Driver { get; }

        public BrowserType Type { get; private set; }

        public void GoToUrl(string url)
        {
            Driver.Url = url;
        }

        public void SetSize()
        {
            Driver.Manage().Window.Size = new Size(2900, 4500); //(1366//800);.Maximize();
            //1366 Desktop size
            //800 Tablet size
            //400 Smartphone
        }
     
        public string GetScreenshotLocation(string filename)
        {
            var unique = Guid.NewGuid().ToString("N").Substring(0, 16);
            return $"{_screenshotsPath}{filename}_{unique}.png";
        }

        public static Browser Create(RunSettings settings)
        {
            IWebDriver driver = null;
            Browser browser = null;
            switch (settings.BrowserType)
            {
                case BrowserType.InternetExplorer:
                    driver = new InternetExplorerDriver("Missing path!!");
                    break;
                case BrowserType.FireFox:
                    driver = new FirefoxDriver();
                    break;
                case BrowserType.Chrome:
                    driver = new ChromeDriver(settings.ChromeDriverPath);
                    break;
                case BrowserType.PhantomJS:
                    var service = PhantomJSDriverService.CreateDefaultService(settings.PhantomDriverPath);
                    service.IgnoreSslErrors = true;
                    service.WebSecurity = false;
                    driver = new PhantomJSDriver(service);
                    break;
            }
            browser = new Browser(driver, settings.BrowserType, settings.ScreenshotsPath);
            return browser;
        }
    }
    public enum BrowserType
    {
        InternetExplorer,
        FireFox,
        Chrome,
        PhantomJS
    }
}