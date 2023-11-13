using OpenQA.Selenium;

namespace Authentication
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private IWebElement UsernameInput => _driver.FindElement(By.Id("username"));
        private IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.TagName("button"));
        private IWebElement ErrorMessage => _driver.FindElement(By.Id("flash-messages"));

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