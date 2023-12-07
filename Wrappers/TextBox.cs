using OpenQA.Selenium;

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
    }
}
