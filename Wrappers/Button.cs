using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using Utilities;

namespace Wrappers
{
    public class Button : WebPageElement
    {
        public Button(IWebElement element) : base(element)
        {
        }

        public Button(By locator) : base(locator)
        {
        }

        public void ClickIfDisplayed(By locator)
        {
            try
            {
                var element = WebDriverExtensions.GetWait(Driver, 5, 200).Until(ExpectedConditions.ElementIsVisible(locator));

                if (element.Displayed)
                {
                    element.Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine($"Element with {locator} did not appear within the specified time.");
            }
        }
    }
}
