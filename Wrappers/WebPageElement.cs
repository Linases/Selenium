using Functionality_Tests_Suit.Constants;
using Functionality_Tests_Suit.FactoryPattern;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Drawing;
using Utilities;

namespace Wrappers
{
    public class WebPageElement : IWebElement
    {
        protected IWebDriver Driver = BrowserFactory.GetDriver(BrowserType.Chrome);
        protected readonly IWebElement Element;

        protected WebPageElement(IWebElement element)
        {
            Element = element;
        }

        protected WebPageElement(By locator)
        {
            Element = Driver.FindElements(locator).FirstOrDefault();
        }

        public string Text => Element.Text;

        public bool Enabled => Element.Enabled;

        public bool Selected => Element.Selected;

        public bool Displayed => Element.Displayed;

        public string TagName => Element.TagName;

        public Point Location => Element.Location;

        public Size Size => Element.Size;

        public void Clear() => Element.Clear();

        public void Click()
        {
            Driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(Element)).Click();
        }

        public void SendKeys(string text) => Element.SendKeys(text);

        public void Submit() => Element.Submit();

        public string GetAttribute(string attributeName) => Element.GetAttribute(attributeName);

        public string GetDomAttribute(string attributeName) => Element.GetDomAttribute(attributeName);

        public string GetDomProperty(string propertyName) => Element.GetDomProperty(propertyName);

        public string GetCssValue(string propertyName) => Element.GetCssValue(propertyName);

        public ISearchContext GetShadowRoot() => Element.GetShadowRoot();

        public IWebElement FindElement(By by)
        {
            var seleiumWebElement = Element.FindElement(by);
            var myWebElement = new WebPageElement(seleiumWebElement);
            return myWebElement;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            var seleniumWebElements = Element.FindElements(by);
            var myWebElements = seleniumWebElements
                .Select(element => (IWebElement)new WebPageElement(element))
                .ToList();
            return myWebElements.AsReadOnly();
        }

        public bool IsElementDisplayed(By locator)
        {
            try
            {
                if (locator == null)
                {
                    throw new ArgumentNullException(nameof(locator));
                }
                else
                {
                    var isDisplayed = WebDriverExtensions.GetWait(Driver).Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
                    return isDisplayed;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element not found.");
                return false;
            }
        }

        public bool AllElementsAreDisplayed(By locator)
        {
            var list = Driver.GetWaitForElementsVisible(locator);
            return list.All(x => x.Displayed);
        }

        public string WaitToGetText(By locator)
        {
            var element = WebDriverExtensions.GetWait(Driver).Until(ExpectedConditions.ElementIsVisible(locator)).Text;
            return element;
        }

        public string GetTextToBePresentInElement(By locator, string text)
        {
            Driver.GetWait().Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
            return Driver.FindElement(locator).Text;
        }

        public bool IsElementDisplayedJs()
        {
            var jsExecutor = (IJavaScriptExecutor)Driver;
            var isElementVisible = (bool)jsExecutor.ExecuteScript(@"
        var rect = arguments[0].getBoundingClientRect();
        return (
            rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        );", Element);
            return isElementVisible;
        }

        public bool IsAvailableToClickButton(By locator)
        {
            Driver.WaitForElementIsVisible(locator);
            if (Element.Enabled && Element.Displayed)
            {
                return true;
            }
            return false;
        }
    }
}
