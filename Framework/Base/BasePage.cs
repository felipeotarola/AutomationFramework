using AutomationFramework.UITests.Framework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.PageObjects;

namespace AutomationFramework.UITests.Framework.Base
{
    public abstract class BasePage
    {
        public Settings Settings { get; set; }
        protected Browser Browser { get; private set; }

        public void SetBrowser(Browser browser)
        {
            Browser = browser;
        }

        protected TPage GetInstance<TPage>() where TPage : BasePage, new()
        {
            var pageInstance = new TPage
            {
                Settings = Settings,
                Browser = Browser
            };
            PageFactory.InitElements(Browser.Driver, pageInstance);

            // Disable CSS animations that causes some UI tests to Fail!
            ((IJavaScriptExecutor)Browser.Driver).ExecuteScript("$('<style type=\"text/css\">* {  transition-property: none !important;  -o-transition-property: none !important;  -moz-transition-property: none !important;  -ms-transition-property: none !important;  -webkit-transition-property: none !important;   transform: none !important;  -o-transform: none !important;  -moz-transform: none !important;  -ms-transform: none !important;  -webkit-transform: none !important;   animation: none !important;  -o-animation: none !important;  -moz-animation: none !important;  -ms-animation: none !important;  -webkit-animation: none !important; }</style>').appendTo('html > head');");

            return pageInstance;
        }
    }
}