using Apache.NMS;
using OpenQA.Selenium;

namespace Welcome
{
    public class WelcomePage
    {
        private readonly IWebDriver _driver;
        private readonly By _formAuthLink = By.XPath("//*[@id='content']//*[text()='Form Authentication']");
        private readonly By _checkboxesLink = By.XPath("//*[@id='content']//*[text()='Checkboxes']");
        private readonly By _dropdownLink = By.XPath("//*[@id='content']//*[text()='Dropdown']");

        public WelcomePage(IWebDriver driver)
        {
            _driver = driver;
            if (!driver.Title.Equals("The Internet"))
            {
                throw new IllegalStateException("This is not The Internet Page," + " current page is: " + driver.Url);
            }
        }

        public void DisplayLoginPage() =>_driver.FindElement(_formAuthLink).Click();
    
        public void DisplayCkeckboxesPage() =>_driver.FindElement(_checkboxesLink).Click();
      
        public void DisplayDropdownPage() =>_driver.FindElement(_dropdownLink).Click();
    }
}