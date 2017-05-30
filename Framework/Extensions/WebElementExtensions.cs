using System;
using System.Collections.Generic;
using System.Linq;
using AutomationFramework.UITests.Framework.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationFramework.UITests.Framework.Extensions
{
    public static class WebElementExtensions
    {
        public static void JsClick(this IWebElement element, Browser browser)
        {
            if (browser.Driver is IJavaScriptExecutor)
            {
                var js = (IJavaScriptExecutor) browser.Driver;
                js.ExecuteScript("arguments[0].click();", element);
            }
            else
            {
                element.Click();
            }
        }

        public static void JsRemove(this IWebElement element, Browser browser)
        {
            if (browser.Driver is IJavaScriptExecutor)
            {
                var js = (IJavaScriptExecutor) browser.Driver;
                js.ExecuteScript("arguments[0].remove();", element);
            }
            else
            {
                element.Click();
            }
        }

        public static void JsMovetoElement(this IWebElement element, Browser browser)
        {
            if (browser.Driver is IJavaScriptExecutor)
            {
                var js = (IJavaScriptExecutor) browser.Driver;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            else
            {
                element.Click();
            }
        }


        public static void JsSendKeysinnerHtml(this IWebElement element, Browser browser, string value)
        {
            if (browser.Driver is IJavaScriptExecutor)
            {
                var js = (IJavaScriptExecutor) browser.Driver;
                js.ExecuteScript("arguments[0].innerHTML ='" + value + "' ", element);
            }
        }

        /*
        JavascriptExecutor myExecutor = ((JavascriptExecutor)driver);
        myExecutor.executeScript("document.getElementsByName('q')[0].value='Kirtesh'", searchbox);
        */

        public static void JsListClick(this IWebElement element, Browser browser)
        {

            var script = "$('.//*[@data-id='core_process_slides']//li[2]').trigger('click')";

            if (browser.Driver is IJavaScriptExecutor)
            {
                var js = (IJavaScriptExecutor) browser.Driver;
                js.ExecuteScript(script, element);
            }
            else
            {
                element.Click();
            }
        }

        public static void JshighLightAnElement(this IWebElement element, Browser browser)
        {
            var script = "arguments[0].style.border='3px dotted blue'";


            if (browser.Driver is IJavaScriptExecutor)
            {
                var js = (IJavaScriptExecutor) browser.Driver;
                js.ExecuteScript(script, element);
            }
            else
            {
                element.Click();
            }
        }


        public static string GetSelectedDropDown(this IWebElement element)
        {
            var ddl = new SelectElement(element);
            return ddl.AllSelectedOptions.First().ToString();
        }

        public static string GetTextOfSelectedDropDown(this IWebElement element)
        {
            var ddl = new SelectElement(element);
            return ddl.AllSelectedOptions.First().Text;
        }

        public static IList<IWebElement> GetSelectedListOptions(this IWebElement element)
        {
            var ddl = new SelectElement(element);
            return ddl.AllSelectedOptions;
        }

        public static string GetLinkText(this IWebElement element)
        {
            return element.Text;
        }

        public static void SelectDropDownList(this IWebElement element, string value)
        {
            var ddl = new SelectElement(element);
            ddl.SelectByText(value);
        }

        public static void AssertElementPresent(this IWebElement element)
        {
            if (!IsElementPresent(element))
                throw new Exception("Element Not Present");
        }

        //Private, can only be used with AssertElementPresent.
        private static bool IsElementPresent(IWebElement element)
        {
            try
            {
                var ele = element.Displayed;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsElementEnabled(this IWebElement element)
        {
            try
            {
                var ele = element.Enabled;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void SelectSettinggroup_AccountPage(this IWebElement element, string value)
        {
            element.FindElement(By.XPath("//ul//li//a[contains(.,'" + value + "')]")).Click();
        }

        public static void SelectCheckbox(this IWebElement element, string value)
        {
            element.FindElement(By.XPath("//label[.='" + value + "']")).Click();
        }

        public static void ContainsText(this IWebElement element, string value)
        {
            element.FindElement(By.XPath("//*[contains(text(),'" + value + "')]")).Click();
        }

        public static void JsClickDocument(this IWebElement element, string value)
        {
            element.FindElement(
                    By.XPath(".//*[@data-id='panel_workspace_list_panel']//a[not(contains(text(),'BANK OF TAIWAN'))]"))
                .Click();
        }
    }
}