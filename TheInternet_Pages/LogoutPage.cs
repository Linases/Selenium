using OpenQA.Selenium;

namespace Authentication
{
    public class LogoutPage
    {
        private readonly IWebDriver _driver;
        private readonly By _logoutButton = By.CssSelector("[href*='logout']");
        private readonly By _logoutMessage = By.Id("flash-messages");

        public LogoutPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickLogoutButton() => _driver.FindElement(_logoutButton).Click();

        public string GetLogoutMessage () => _driver.FindElement(_logoutMessage).Text;
    }
}
