using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Utilities
{
    public static class WebDriverExtensions
    {
        private static IWebDriver _driver;


        public static IWebElement WaitForElementVisible(this IWebDriver driver, By locator, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static IList<IWebElement> WaitForElementsVisible(this IWebDriver driver, By locator, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        public static IWebElement WaitForElementClicable(this IWebDriver driver, By locator, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public static IWebElement NoSuchElementExceptionWait(this IWebDriver driver, By locator, int timeoutInSeconds, int miliSeconds)
        {
            var fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(miliSeconds);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return fluentWait.Until(driver => driver.FindElement(locator));

        }
        public static IWebElement StaleElementExceptionWait(this IWebDriver driver, By locator, int timeoutInSeconds = 20, int miliSeconds = 500)
        {
            var fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(miliSeconds);
            fluentWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return fluentWait.Until(driver => driver.FindElement(locator));

        }

    }
}

