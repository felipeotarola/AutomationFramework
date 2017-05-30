using AutomationFramework.UITests.Framework.Config;
using TechTalk.SpecFlow;

namespace AutomationFramework.UITests.Framework.Base
{
    [Binding]
    public class SpecTestInitializeHook
    {
        private readonly ScenarioContext _scenarioContext;

        private Browser _browser;

        public SpecTestInitializeHook(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public static Settings CurrentSettings { get; private set; }

        [BeforeTestRun]
        public static void TestRunStart()
        {
            CurrentSettings = new TestConfiguration().Settings;
        }

        [AfterTestRun]
        public static void TestRunStop()
        {
        }

        [BeforeScenario]
        public void TestScenarioStart()
        {
            _browser = Browser.Create(CurrentSettings.RunSettings);
            _scenarioContext["SeleniumBrowser"] = _browser;
        }

        [AfterScenario]
        public void TestScenarioStop()
        {
            _browser.Driver.Quit();
        }
    }
}