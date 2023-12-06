using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Drawing;
using Utilities;

namespace Wrappers
{
    public class WebPageElement : IWebElement

    {
        protected IWebDriver Driver;
        protected IWebElement Element;

        public WebPageElement(IWebElement element)
        {
            Element = element;
        }

        public WebPageElement(IWebDriver driver, By locator)
        {
            Driver = driver;
            Element = Driver.FindElements(locator).FirstOrDefault();
        }

        public WebPageElement()
        {
        }

        public string Text => Element.Text;

        public bool Enabled => Element.Enabled;

        public bool Selected => Element.Selected;

        public bool Displayed => Element.Displayed;

        public string TagName => Element.TagName;

        public Point Location => Element.Location;

        public Size Size => Element.Size;

        public void Clear() => Element.Clear();

        public void Click() => Element.Click();

        public void SendKeys(string text) => Element.SendKeys(text);

        public void Submit() => Element.Submit();

        public string GetAttribute(string attributeName) => Element.GetAttribute(attributeName);

        public string GetDomAttribute(string attributeName) => Element.GetDomAttribute(attributeName);

        public string GetDomProperty(string propertyName) => Element.GetDomProperty(propertyName);

        public string GetCssValue(string propertyName) => Element.GetCssValue(propertyName);

        public ISearchContext GetShadowRoot() => Element.GetShadowRoot();

        public IWebElement FindElement(By by) => Element.FindElement(by);

        public ReadOnlyCollection<IWebElement> FindElements(By by) => Element.FindElements(by);

        public bool IsElementDisplayedTryCatch(IWebDriver driver, By locator)
        {
            try
            {
                if (locator == null)
                {
                    Console.WriteLine("No elements available");
                    return true;
                }
                else
                {
                    var isDisplayed = WebDriverExtensions.GetWait(driver).Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
                    return isDisplayed;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element not found.");
                return false;
            }
        }

        public bool IsElementDisplayed(IWebDriver driver, By locator)
        {
            var isDisplayed = driver.WaitForElementIsVisible(locator).Displayed;
            return isDisplayed;
        }

        public string WaitToGetText(IWebDriver driver, By locator)
        {
            var element = WebDriverExtensions.GetWait(driver).Until(ExpectedConditions.ElementIsVisible(locator)).Text;
            return element;
        }

        public bool IsListDisplayed(IWebDriver driver, By locator)
        {
            var list = driver.GetWaitForElementsVisible(locator);
            var isDisplayed = list.Where(x => x.Displayed).ToList();
            return isDisplayed.Any();
        }

        public bool IsElementDisplayedJs(IWebDriver driver, IWebElement element)
        {
            var jsExecutor = (IJavaScriptExecutor)driver;
            var isElementVisible = (bool)jsExecutor.ExecuteScript(@"
        var rect = arguments[0].getBoundingClientRect();
        return (
            rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        );", element);
            return isElementVisible;
        }

        public bool IsAvailableToClickButton(IWebElement element)
        {
            if (element.Enabled && element.Displayed)
            {
                return true;
            }
            return false;
        }
    }
}