using AutomationFramework.UITests.Framework.Base;
using AutomationFramework.UITests.UITest.Smoke;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AutomationFramework.UITests.UITest.Smoke.Specflow
{
    [Binding]
    public class SpecFlowSteps : BaseSpecStep
    {
        public SpecFlowSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}