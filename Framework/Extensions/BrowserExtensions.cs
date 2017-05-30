using System;
using System.IO;
using AutomationFramework.UITests.Framework.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.UITests.Framework.Extensions
{
    public static class BrowserExtensions
    {
        public static void TakeScreenshot(this Browser browser, string filename)
        {
            var screenshotHandler = browser.Driver as ITakesScreenshot;
            if (screenshotHandler != null)
            {
                var screenshot = screenshotHandler.GetScreenshot();
                var path = browser.GetScreenshotLocation(filename);
                var file = new FileInfo(path);
                if (file.Directory != null && !file.Directory.Exists)
                {
                    file.Directory.Create();
                }
                screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            }
        }

        public static void WaitForElement(this Browser browser, IWebElement element, int seconds = 20)
        {
            var wait = new WebDriverWait(browser.Driver, new TimeSpan(0, 0, seconds));
            wait.Until(d => element.Displayed);
        }

        public static void WaitAndClick(this Browser browser, IWebElement element, int seconds = 20)
        {
            var wait = new WebDriverWait(browser.Driver, new TimeSpan(0, 0, seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));

            element.Click();
        }
        public static void WaitAndSelect(this Browser browser, IWebElement element, int seconds = 20)
        {
            var wait = new WebDriverWait(browser.Driver, new TimeSpan(0, 0, seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));

            element.Click();
        }

        public static void WaitAndJsClick(this Browser browser, IWebElement element, int seconds = 20)
        {
            var wait = new WebDriverWait(browser.Driver, new TimeSpan(0, 0, seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));

            element.JsClick(browser);
        }





        public static IWebElement FindElementByJs(this IWebDriver driver, string jsCommand)
        {
            return (IWebElement) ((IJavaScriptExecutor) driver).ExecuteScript(jsCommand);
        }

        public static IWebElement FindElementByJsWithWait(this Browser browser, string jsCommand, int timeoutInSeconds = 20)
        {
            var driver = browser.Driver;
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(d => d.FindElementByJs(jsCommand));
            }
            return driver.FindElementByJs(jsCommand);
        }

      
    }
}