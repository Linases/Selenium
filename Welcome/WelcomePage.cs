using Apache.NMS;
using OpenQA.Selenium;

namespace Welcome
{
    public class WelcomePage
    {
        private readonly IWebDriver _driver;
        private readonly By formAuthLink = By.XPath("//*[@id='content']//*[text()='Form Authentication']");
        private readonly By checkboxesLink = By.XPath("//*[@id='content']//*[text()='Checkboxes']");

        public WelcomePage(IWebDriver driver)
        {
            _driver = driver;
            if (!driver.Title.Equals("The Internet"))
            {
                throw new IllegalStateException("This is not The Internet Page," + " current page is: " + driver.Url);
            }
        }

        public void DisplayLoginPage()
        {

            _driver.FindElement(formAuthLink).Click();
        }

        public void DisplayCkeckboxesPage()
        {
            _driver.FindElement(checkboxesLink).Click();
        }
    }
}