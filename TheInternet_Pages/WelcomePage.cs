using Apache.NMS;
using OpenQA.Selenium;

namespace Welcome
{
    public class WelcomePage
    {
        private readonly IWebDriver _driver;
        private IWebElement FormAuthLink => _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Form Authentication']"));
        private IWebElement CheckboxesLink => _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Checkboxes']"));
        private IWebElement DropdownLink => _driver.FindElement(By.XPath("//*[@id='content']//*[text()='Dropdown']"));

        public WelcomePage(IWebDriver driver)
        {
            _driver = driver;
            if (!driver.Title.Equals("The Internet"))
            {
                throw new IllegalStateException("This is not The Internet Page," + " current page is: " + driver.Url);
            }
        }

        public void OpenLoginPage() => FormAuthLink.Click();

        public void OpenCkeckboxesPage() => CheckboxesLink.Click();

        public void OpenDropdownPage() => DropdownLink.Click();
    }
}