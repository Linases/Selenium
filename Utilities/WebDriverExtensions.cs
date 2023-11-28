using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Utilities
{
    public static class WebDriverExtensions
    {
        private static IWebDriver _driver;

        public static WebDriverWait GetWait(
        this IWebDriver driver,
        int timeOutSeconds = 20,
        int pollingIntervalMilliseconds = 300,
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
            typeof(WebDriverException),
            typeof(InvalidOperationException),

        };
            var exceptions = exceptionsToIgnore ?? exceptionsToIgnoreByDefault;
            wait.IgnoreExceptionTypes(exceptions);
            return wait;
        }

        public static IList<IWebElement> GetWaitForElementsVisible(this IWebDriver driver, By locator)
        {
            var wait = driver.GetWait();
            return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }

        public static IWebElement GetWaitForElementIsVisible(this IWebDriver driver, By locator)
        {
            var wait = driver.GetWait();
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static IWebElement GetWaitForElementIsClicable(this IWebDriver driver, By locator)
        {
            var wait = driver.GetWait();
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        public static IWebElement WaitForElementIsVisible(this IWebDriver driver, By locator, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static IList<IWebElement> WaitForElementsVisible(this IWebDriver driver, By locator, int timeoutInSeconds = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        public static IWebElement WaitForElementIsClicable(this IWebDriver driver, By locator, int timeoutInSeconds = 20)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }
    }
}

