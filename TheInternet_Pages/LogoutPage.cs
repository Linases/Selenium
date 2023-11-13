using OpenQA.Selenium;

namespace Authentication
{
    public class LogoutPage
    {
        private readonly IWebDriver _driver;
        private readonly By _logoutButton = By.CssSelector("[href*='logout']");
        private readonly By _logoutMessage = By.Id("flash-messages");
        private IWebElement LogoutButton => _driver.FindElement(_logoutButton);
        private IWebElement LogoutMessage => _driver.FindElement(_logoutMessage);

        public LogoutPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickLogoutButton() => LogoutButton.Click();

        public string GetLogoutMessage () => LogoutMessage.Text;
    }
}
