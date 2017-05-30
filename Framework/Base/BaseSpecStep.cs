using System;
using AutomationFramework.UITests.Framework.Config;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace AutomationFramework.UITests.Framework.Base
{
    public abstract class BaseSpecStep
    {
        public readonly ScenarioContext ScenarioContext;

        protected BaseSpecStep(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext ??
            throw new ArgumentNullException(nameof(scenarioContext));
        }

        protected Browser CurrentBrowser => (Browser) ScenarioContext["SeleniumBrowser"];

        protected Settings Settings => SpecTestInitializeHook.CurrentSettings;

        public void SetCurrentPage<TPage>(TPage page)
        {
            ScenarioContext["currentPage"] = page;
        }

        protected TPage GetCurrentPage<TPage>() where TPage : BasePage
        {
            return (TPage) ScenarioContext["currentPage"];
        }

        public virtual void NavigateSite(string url)
        {
            CurrentBrowser.SetSize();
            CurrentBrowser.GoToUrl(url);
        }

        protected TPage GetInstance<TPage>() where TPage : BasePage, new()
        {
            var pageInstance = new TPage {Settings = Settings};
            pageInstance.SetBrowser(CurrentBrowser);
            PageFactory.InitElements(CurrentBrowser.Driver, pageInstance);
            return pageInstance;
        }
    }
}