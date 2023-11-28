using OpenQA.Selenium;
using Utilities;

namespace BookingPages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        By EnterPasswordField => (By.XPath("//*[@placeholder='Enter a password']"));
        private IWebElement LoginButton => _driver.FindElement(By.XPath("//*[@data-testid='header-sign-in-button']"));
        private IWebElement EmailField => _driver.FindElement(By.Id("username"));
        private IWebElement ContinueWithEmailButton => _driver.FindElement(By.XPath("//*[text()='Continue with email']"));
        private IWebElement ConfirmPasswordField => _driver.FindElement(By.XPath("//*[@placeholder='Confirm your password']"));

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
            var password = _driver.WaitForElementIsVisible(EnterPasswordField, 20);
            var isVisible = password.Displayed;
            return isVisible;
        }

        public bool IsConfirmPasswordFieldVisible()
        {
            var isVisible = ConfirmPasswordField.Displayed; 
            return isVisible;
        }
    }
}
