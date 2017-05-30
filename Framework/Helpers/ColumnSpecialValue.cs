using System.Collections.Generic;
using OpenQA.Selenium;

namespace AutomationFramework.UITests.Framework.Helpers
{
    public class ColumnSpecialValue
    {
        public IEnumerable<IWebElement> ElementCollection { get; set; }
        public string ControlType { get; set; }
    }
}