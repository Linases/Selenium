using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wrappers
{
    public class DropDown : WebPageElement
    {

        public DropDown()
        {
        }

        public DropDown(IWebElement webElement) : base(webElement)
        {
        }

        public void SelectByText(string text)
        {
            var selectElement = new SelectElement(this.Element);
            selectElement.SelectByText(text);
        }

        public void SelectByValue(string value)
        {
            var selectElement = new SelectElement(Element);
            selectElement.SelectByValue(value);
        }

        public void SelectByIndex(int index)
        {
            var selectElement = new SelectElement(Element);
            selectElement.SelectByIndex(index);
        }

        public IList<IWebElement> GetOptions()
        {
            var selectElement = new SelectElement(Element);
            var allElements = selectElement.Options;
            return allElements;
        }

        public void SelectFromListByValue(IWebDriver driver, By locator, string value)
        {
            var list = WebDriverExtensions.GetWait(driver).Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
            if (list.Count > 0)
            {
                var elementValue = list.Select(element => new SelectElement(element)).ToList();
                elementValue[0].SelectByValue(value);
            }
        }
    }
}

