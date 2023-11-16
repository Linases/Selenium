using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Utilities
{
    public static class WebDriverExtensions
    {
        private static IWebDriver _driver;

        public static void ImplicitWait (this IWebDriver driver, int timeoutInSeconds)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutInSeconds);
        }
        public static IWebElement WaitForElementVisible(this IWebDriver driver, By locator, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static IList<IWebElement> WaitForElementsVisible(this IWebDriver driver, By locator, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        public static IWebElement WaitForElementClicable(this IWebDriver driver, By locator, int timeoutInSeconds)
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
         public static IWebElement StaleElementExceptionWait(this IWebDriver driver, By locator, int timeoutInSeconds, int miliSeconds)
        {
            var fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(miliSeconds);
            fluentWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return fluentWait.Until(driver => driver.FindElement(locator));

        }

        public static WebDriverWait GetWait(
        this IWebDriver driver,
        int timeOutSeconds = 10,
        int pollingIntervalMilliseconds = 250,
        Type[]? exceptionsToIgnore = null)

        {
            var timeOut = TimeSpan.FromSeconds(timeOutSeconds);
            var pollingInterval = TimeSpan.FromMilliseconds(pollingIntervalMilliseconds);
            var clock = new SystemClock();
            var wait = new WebDriverWait(clock, driver, timeOut, pollingInterval);

            var exceptionsToIgnoreByDefault = new[]
            {
            typeof(StaleElementReferenceException),
            typeof(NoSuchElementException),
            typeof(ElementClickInterceptedException),
            typeof(ElementNotInteractableException),
        };

            wait.IgnoreExceptionTypes(exceptionsToIgnore ?? exceptionsToIgnoreByDefault);
            return wait;
        }
    }
}
