using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Wrappers
{
    public class DropDown : WebPageElement
    {

        public DropDown()
        {
        }

        public DropDown(IWebElement webElement): base(webElement) 
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
    }
}
