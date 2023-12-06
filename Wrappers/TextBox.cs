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

        public TextBox()
        {
        }

        public void ClearAndEnterText(string text)
        {
            Element.Clear();
            Element.SendKeys(text);
        }

      public void DeleteAllTextWithKey(IWebElement element)
        {
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Backspace);
        }

        public string GetTextWithJsById(IWebDriver driver, string attributeName)
        {
            var js = (IJavaScriptExecutor)driver;
            var stringValue = (string)js.ExecuteScript($"return document.getElementById('{attributeName}').value;");
            return stringValue;
        }
    }
}
