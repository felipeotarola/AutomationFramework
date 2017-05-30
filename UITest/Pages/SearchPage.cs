using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutomationFramework.UITests.Framework.Base;
using AutomationFramework.UITests.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using Xunit;
namespace AutomationFramework.UITests.UITest.Smoke.Pages
{
   public class SearchPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = ".//*[@id='lst-ib']")]
         IWebElement SearchInput { get; set; }

        public SearchPage Send_Value_To_Search_Input(string value)
        {
            SearchInput.SendKeys(value);
            return this;
        }
    }
}
