using OpenQA.Selenium;

namespace Authentication
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly By usernameInput = By.Id("username");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.TagName("button");
        private readonly By errormessage = By.Id("flash-messages");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterUsername(string username) => _driver.FindElement(usernameInput).SendKeys(username);

        public void EnterPassword(string passsword) => _driver.FindElement(passwordInput).SendKeys(passsword);

        public void ClickLoginButton() => _driver.FindElement(loginButton).Click();

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        public string InvalidLogin(string username, string password)
        {
           Login (username, password);
            return GetErrorMessage();
        }

        public string GetErrorMessage()
        {
            return _driver.FindElement(errormessage).Text;
        }
    }
}