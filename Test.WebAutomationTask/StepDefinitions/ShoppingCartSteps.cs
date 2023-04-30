using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using Test.WebAutomationTask.Pages;
using Test.WebAutomationTask.SetUp;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace Test.WebAutomationTask.StepDefinitions
{
    [Binding]
    public class ShoppingCartSteps
    {
        private Context _context;
        private ShoppingCartPage _page;
        static ExtentTest feature;
        static ExtentTest scenario;
        static ExtentReports report;
        ScenarioContext _scenarioContext;


        public ShoppingCartSteps(Context context, ShoppingCartPage page, ScenarioContext scenarioContext)
        {
            _context = context;
            _page = page;
            _scenarioContext = scenarioContext;
        }


        [Given(@"I add four random items to my cart")]
        public void GivenIAddFourRandomItemsToMyCart()
        {
            _context.LoadApplicationUnderTest();
            scenario = feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            _page.AddFourRandomItems();
        }


        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            _page.ViewCart();
        }

        [Then(@"I find total (.*) items listed in my cart")]
        [Then(@"I am able to verify (.*) items in my cart")]
        public void ThenIFindTotalFourItemsListedInMyCart(int expectedResult)
        {
            Assert.AreEqual(expectedResult, _page.VerifyCartItems());
        }

        [When(@"I search for the lowest price item")]
        public void WhenISearchForTheLowestPriceItem()
        {
            _page.GetLowestPrice();
        }

        [When(@"I am able to remove the lowest price item from my cart")]
        public void WhenIAmAbleToRemoveTheLowesrPriceItemFromMyCart()
        {
            _page.RemoveLowestPriceItem();
            Thread.Sleep(1500);
        }






        [BeforeTestRun]
        public static void ReportGenerator()
        {
            var testResultReport = new ExtentHtmlReporter(AppDomain.CurrentDomain.BaseDirectory);
            testResultReport.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            report = new ExtentReports();
            report.AttachReporter(testResultReport);
        }


        [AfterTestRun]
        public static void ReportCleaner()
        {
            report.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            feature = report.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterStep]
        public void StepsInTheReport()
        {
            var typeOfStep = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            //Cater for a step that passed
            if (_scenarioContext.TestError == null)
            {
                if (typeOfStep.Equals("Given"))
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (typeOfStep.Equals("When"))
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (typeOfStep.Equals("Then"))
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                }
            }
            //Cater for a step that failed
            if (_scenarioContext.TestError != null)
            {
                if (typeOfStep.Equals("Given"))
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                }
                else if (typeOfStep.Equals("When"))
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                }
                else if (typeOfStep.Equals("Then"))
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                }
            }
            //Cater for a step that has not been implemented
            if (_scenarioContext.ScenarioExecutionStatus.ToString().Equals("StepDefinitionPending"))
            {
                if (typeOfStep.Equals("Given"))
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
                else if (typeOfStep.Equals("When"))
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
                else if (typeOfStep.Equals("Then"))
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                }
            }
        }

        [AfterScenario]
        public void CloseApplicationUnderTest()
        {
            try
            {
                if (_scenarioContext.TestError != null)  //this condition will always be true when a test failed
                {
                    string scenarioName = _scenarioContext.ScenarioInfo.Title;
                    string directory = AppDomain.CurrentDomain.BaseDirectory + @"\Screenshots\";
                    _context.TakeScreenshotAtThePointOfTestFailure(directory, scenarioName);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _context.ShutDownApplicationUnderTest();
            }
        }
    }
}
