using OpenQA.Selenium;

namespace Authentication
{
    public class LogoutPage
    {
        private readonly IWebDriver _driver;
        private readonly By logoutButton = By.CssSelector("[href*='logout']");
        private readonly By logoutMessage = By.Id("flash-messages");

        public LogoutPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickLogoutButton() => _driver.FindElement(logoutButton).Click();

        public string GetLogoutMessage ()
        {
            return _driver.FindElement(logoutMessage).Text;
        }
    }
}
