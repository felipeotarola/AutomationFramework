using System;
using System.Diagnostics;
using OpenQA.Selenium;

namespace AutomationFramework.UITests.Framework.Extensions
{
    public static class WebDriverExtensions
    {
        public static void WaitForPageLoaded(this IWebDriver driver)
        {
            driver.WaitForCondition(drv =>
            {
                var state = drv.ExecuteJs("return document.readyState").ToString();
                return state == "complete";
            }, 10);
        }

        public static void WaitForCondition<T>(this T obj, Func<T, bool> condition, int timeOut)
        {
            Func<T, bool> execute =
                arg =>
                {
                    try
                    {
                        return condition(arg);
                    }
                    catch
                    {
                        return false;
                    }
                };

            var stopWatch = Stopwatch.StartNew();
            while (stopWatch.ElapsedMilliseconds < timeOut)
                if (execute(obj))
                    break;
        }

        internal static object ExecuteJs(this IWebDriver driver, string script)
        {
            return ((IJavaScriptExecutor) driver).ExecuteScript(script);
        }
    }
}