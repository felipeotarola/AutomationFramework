using System;
using System.Collections.Generic;
using System.IO;
using AutomationFramework.UITests.Framework.Config;
using AutomationFramework.UITests.Framework.Helpers;
using AutomationFramework.UITests.UITest.Pages;
using OpenQA.Selenium.Support.PageObjects;
using Xunit.Abstractions;


namespace AutomationFramework.UITests.Framework.Base
{
    public class BaseUITest : IDisposable
    {
        protected readonly Browser CurrentBrowser;
        protected readonly List<Datacollection> ExcelData;
        protected readonly ITestOutputHelper Output;
        protected readonly Settings Settings;

        private FileStream _stream;
        protected StreamWriter Logger;

        protected string LogPath;

        public BaseUITest(ITestOutputHelper output, TestConfiguration configuration)
        {
            this.Output = output;
            this.Settings = configuration.Settings;
            CurrentBrowser = Browser.Create(Settings.RunSettings);
            CurrentBrowser.SetSize();
            SetupLogging();
            ExcelData = GetExcelData(configuration);
        }

        private List<Datacollection> GetExcelData(TestConfiguration configuration)
        {
            var filename = Environment.CurrentDirectory + configuration.Settings.RunSettings.ExcelDataPath;
            return ExcelHelpers.PopulateInCollection(filename);
        }

        private void SetupLogging()
        {
            LogPath = $"Logs\\{Guid.NewGuid():N}.log";
            var fileInfo = new FileInfo(LogPath);
            if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
            _stream = new FileStream(LogPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            Logger = new StreamWriter(_stream);
        }

        public TPage Login_And_Go_To_Page<TPage>(string url) where TPage : BasePage, new()
        {
            return Login_And_Go_To_Page<TPage>(url, Settings.RunSettings.MicrosoftEmail, Settings.RunSettings.MicrosoftPassword);
        }

        private TPage Login_And_Go_To_Page<TPage>(string url, string userName, string password) where TPage : BasePage, new()
        {
            CurrentBrowser.Driver.Navigate().GoToUrl(url);
            Logger.WriteLine($"Navigated to {url}");
            return GetInstance<LoginPage>()
                .TraditionalLogin<TPage>(userName, password);
        }


        public TPage Go_To_Page<TPage>(string url) where TPage : BasePage, new()
        {
            return Go_To_Page<TPage>(url, Settings.RunSettings.MicrosoftEmail, Settings.RunSettings.MicrosoftPassword);
        }

        private TPage Go_To_Page<TPage>(string url, string userName, string password) where TPage : BasePage, new()
        {
            CurrentBrowser.Driver.Navigate().GoToUrl(url);
            return GetInstance<TPage>();
        }

        protected TPage GetInstance<TPage>() where TPage : BasePage, new()
        {
            var pageInstance = new TPage
            {
                Settings = Settings
            };
            pageInstance.SetBrowser(CurrentBrowser);
            PageFactory.InitElements(CurrentBrowser.Driver, pageInstance);
            return pageInstance;
        }

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Logger.Dispose();
                    _stream.Dispose();
                    CurrentBrowser.Driver.Quit();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WebTests() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}