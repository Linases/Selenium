using OpenQA.Selenium;

namespace Authentication
{
    public class LogoutPage
    {
        private readonly IWebDriver _driver;
        private IWebElement LogoutButton => _driver.FindElement(By.CssSelector("[href*='logout']"));
        private IWebElement LogoutMessage => _driver.FindElement(By.Id("flash-messages"));

        public LogoutPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickLogoutButton() => LogoutButton.Click();

        public string GetLogoutMessage() => LogoutMessage.Text;
    }
}
