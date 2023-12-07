using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Utilities;

namespace Wrappers
{
    public class TextBox : WebPageElement
    {
        public TextBox(IWebElement element) : base(element)
        {
        }

        public TextBox(By locator) : base(locator)
        {
        }

        public void ClearAndEnterText(string text)
        {
            Element.Clear();
            Element.SendKeys(text);
        }

        public void DeleteAllTextWithKey()
        {
            Element.SendKeys(Keys.Control + "a");
            Element.SendKeys(Keys.Backspace);
        }

        private void DeleteAndEnterText(string text, By locator)
        {
            Element = WebDriverExtensions.GetWait(Driver).Until(ExpectedConditions.ElementToBeClickable(locator));
            Click();
            if (Element.GetAttribute("value").Length > 0)
            {
                DeleteAllTextWithKey();
            }
            if (Element.Enabled)
            {
                SendKeys(text);
            }
        }
    }
}
