using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V117.Audits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BookingPages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private IWebElement LoginButton => _driver.NoSuchElementExceptionWait(By.XPath("//*[@data-testID='header-sign-in-button']"), 30, 500);
        private IWebElement EmailField => _driver.WaitForElementVisible(By.Id("username"), 20);
        private IWebElement ContinueWithEmailButton => _driver.WaitForElementClicable(By.XPath("//*[text()='Continue with email']"), 20);

        private IWebElement PasswordField => _driver.WaitForElementVisible(By.XPath("//*[@placeholder='Enter a password']"), 20);
        private IWebElement ConfirmPasswordField => _driver.WaitForElementVisible(By.XPath("//*[@placeholder='Confirm your password']"), 20);

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickSignIn() => LoginButton.Click();

        public void EnterEmail(string email) => EmailField.SendKeys(email);

        public string GetEmail() => EmailField.GetAttribute("value");

        public void ClickContinueButton() => ContinueWithEmailButton.Click();

        public bool IsPasswordFieldVisible () => PasswordField.Displayed;

        public bool IsConfirmPasswordFieldVisible() => ConfirmPasswordField.Displayed;

    }
}
