using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Authentication
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly By _usernameInput = By.Id("username");
        private readonly By _passwordInput = By.Id("password");
        private readonly By _loginButton = By.TagName("button");
        private readonly By _errorMessage = By.Id("flash-messages");
        private IWebElement UsernameInput => _driver.FindElement(_usernameInput);
        private IWebElement PasswordInput => _driver.FindElement(_passwordInput);
        private IWebElement LoginButton => _driver.FindElement(_loginButton);
        private IWebElement ErrorMessage => _driver.FindElement(_errorMessage);

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterUsername(string username) => UsernameInput.SendKeys(username);

        public void EnterPassword(string passsword) => PasswordInput.SendKeys(passsword);

        public void ClickLoginButton() => LoginButton.Click();

        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
        }

        public string InvalidLogin(string username, string password)
        {
            Login(username, password);
            return GetErrorMessage();
        }

        public string GetErrorMessage() => ErrorMessage.Text;
    }
}