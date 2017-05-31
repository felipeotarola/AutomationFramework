using AutomationFramework.UITests.Framework.Base;
using AutomationFramework.UITests.Framework.Config;
using AutomationFramework.UITests.Framework.Helpers;
using AutomationFramework.UITests.UITest.Smoke.Pages;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AutomationFramework.UITests.UITest.Pages
{
    [Collection("TestConfigurationCollection")]
    public class SearchPage_Fact : BaseUITest
    {
        public SearchPage_Fact(ITestOutputHelper output, TestConfiguration configuration) : base(output, configuration)
        {
        }

        [Fact]
        public void Search_In_Google() 
        {   //Arrange
            var value = "Hello Juulia";
            var homePageUrl = Settings.RunSettings.FrontWebUrl;

            //Act
            var searchpage = Go_To_Page<SearchPage>(homePageUrl)
                .Send_Value_To_Search_Input(value);

            //Assert
             
        }

    }
}
