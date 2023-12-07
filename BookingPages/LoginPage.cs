using OpenQA.Selenium;
using Wrappers;

namespace BookingPages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        private By EnterPasswordField => (By.XPath("//*[@placeholder='Enter a password']"));
        private By ConfirmPasswordField => (By.XPath("//*[@placeholder='Confirm your password']"));
        private Button LoginButton => new Button(_driver.FindElement(By.XPath("//*[@data-testid='header-sign-in-button']")));
        private TextBox EmailField => new TextBox(_driver.FindElement(By.Id("username")));
        private Button ContinueWithEmailButton => new Button(_driver.FindElement(By.XPath("//*[text()='Continue with email']")));

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickSignIn() => LoginButton.Click();

        public void EnterEmail(string email) => EmailField.SendKeys(email);


        public string GetEmail() => EmailField.GetAttribute("value");

        public void ClickContinueButton() => ContinueWithEmailButton.Click();

        public bool IsPasswordFieldVisible()
        {
            var passwordField = new TextBox(EnterPasswordField);
            var isVisible = passwordField.IsElementDisplayed(EnterPasswordField);
            return isVisible;
        }

        public bool IsConfirmPasswordFieldVisible()
        {
            var confirmPasswordField = new TextBox(ConfirmPasswordField);
            var isVisible = confirmPasswordField.IsElementDisplayed(EnterPasswordField);
            return isVisible;
        }
    }
}
