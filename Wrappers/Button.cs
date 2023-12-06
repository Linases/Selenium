using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Utilities;

namespace Wrappers
{
    public class Button : WebPageElement
    {
        public Button(IWebElement element) : base(element)
        {
        }

        public Button()
        {
        }

        public void ClickWhenReady(IWebDriver driver,IWebElement element)
        {
            driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }

        public void ClickWhenReady(IWebDriver driver,By locator)
        {
            Element= driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(locator));
            Element.Click();
        }

        public void ClickLastFromList(IWebDriver driver, By locator)
        {
            var list = driver.GetWaitForElementsVisible(locator);
            Element = list.LastOrDefault(x => x.Displayed);
            Element.Click();
        }

        public void ClickFirstFromList(IWebDriver driver, By locator)
        {
            var list = driver.GetWaitForElementsVisible(locator);
            Element = list.FirstOrDefault(x => x.Displayed);
            Element.Click();
        }     
        public void ClickFirstFromListWithElementIncluded(IWebDriver driver, By listLocator, By elementLocator)
        {
            var list = driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(listLocator));
            Element = list.FirstOrDefault(option => option.Displayed).FindElement(elementLocator);
            Element.Click();
        }
        public void ClickFirstThatContainsText(IWebDriver driver, By locator, string text)
        {
            var list = driver.WaitForElementsVisible(locator);
            Element = list.FirstOrDefault(element => element.Text.Contains($"{text}"));
            Element.Click();
        }

        public void ClickFirstThatContainsText(IList<IWebElement> webElements,string text)
        {
            Element = webElements.FirstOrDefault(element => element.Text.Contains($"{text}"));
            Element.Click();
        }

        public void ClickWhenDoNotContainTextInList(ReadOnlyCollection<IWebElement> webElements, string text, IWebElement element)
        {
            var currentMonthYearText = webElements.Select(x => x.Text).ToList();
            
            while (!currentMonthYearText.Contains(text))
            {
                element.Click();
            }
        }

        public void ClickWhenDoNotContainText(IWebElement webElement, string text, IWebElement element)
        {
            var currentMonthYearText = webElement.Text;

            while (!currentMonthYearText.Contains(text))
            {
                element.Click();
            }
        }

        public void ClickIfDisplayedTryCatch(IWebDriver driver, By locator)
        {
            try
            {
                var element = WebDriverExtensions.GetWait(driver, 5, 200).Until(ExpectedConditions.ElementIsVisible(locator));

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
