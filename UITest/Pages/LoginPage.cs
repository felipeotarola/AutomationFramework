using System.Threading;
using AutomationFramework.UITests.Framework.Base;
using AutomationFramework.UITests.Framework.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
namespace AutomationFramework.UITests.UITest.Pages
{
    public class LoginPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id='login']/input")]
        private IWebElement Login { get; set; }

        [FindsBy(How = How.XPath, Using = ".//*[@id='password']/input")]
        private IWebElement Password { get; set; }


        public TPage TraditionalLogin<TPage>(string login, string password) where TPage : BasePage, new()
        {
            TraditionalLoginMicrosoft(login, password);
            return GetInstance<TPage>();
        }


        private LoginPage TraditionalLoginMicrosoft(string login, string password)
        { 
            Login.SendKeys(login);
            Password.SendKeys(password);
              return this;
        }
    }
}