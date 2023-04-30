using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;

namespace Test.WebAutomationTask.SetUp
{
    public static class WebDriverExtension
    {

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        internal static void Click(this IWebDriver driver, By identifier)
        {
            driver.FindElement(identifier, 5).Click();
        }

    }
}
