using OpenQA.Selenium;

namespace Authentication
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly By _usernameInput = By.Id("username");
        private readonly By _passwordInput = By.Id("password");
        private readonly By _loginButton = By.TagName("button");
        private readonly By _errorMessage = By.Id("flash-messages");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterUsername(string username) => _driver.FindElement(_usernameInput).SendKeys(username);

        public void EnterPassword(string passsword) => _driver.FindElement(_passwordInput).SendKeys(passsword);

        public void ClickLoginButton() => _driver.FindElement(_loginButton).Click();

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

        public string GetErrorMessage() => _driver.FindElement(_errorMessage).Text;
    }
}