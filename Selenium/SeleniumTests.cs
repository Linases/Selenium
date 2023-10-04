using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;

namespace Selenium
{
    [TestFixture]
    public class SeleniumTests
    {
        private IWebDriver driver;

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
        [Test, Order(3)]
        public void SuccessfulLogin()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var elementUserName = driver.FindElement(By.Id("user-name"));
            elementUserName.SendKeys("standard_user");
            var elementPassword = driver.FindElement(By.Id("password"));
            elementPassword.SendKeys("secret_sauce");

            var elementLoginButton = driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();

            string pageUrl = driver.Url;
            Assert.That(pageUrl, Is.EqualTo("https://www.saucedemo.com/inventory.html"));

            var elementItem = driver.FindElement(By.Id("item_4_title_link"));
            elementItem.Click();

            string itemUrl = driver.Url;
            Assert.That(itemUrl, Is.EqualTo("https://www.saucedemo.com/inventory-item.html?id=4"));
        }
        [Test, Order(1)]
        public void FailedLoginWithEmptyCredentials()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var elementUserName = driver.FindElement(By.Id("user-name"));
            var elementPassword = driver.FindElement(By.Id("password"));
            var elementLoginButton = driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();
            var errorMessage = driver.FindElement(By.CssSelector("[data-test*='error']"));
            Assert.That(errorMessage.Text.Contains("Username is required"));
        }
        [Test, Order(2)]
        public void FailedLoginWithInvalidCredentials() 
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var elementUserName = driver.FindElement(By.Id("user-name"));
            elementUserName.SendKeys("invalid_user");
            var elementPassword = driver.FindElement(By.Id("password"));
            elementPassword.SendKeys("invalid_password");
            var elementLoginButton = driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();
            var errorMessage = driver.FindElement(By.CssSelector("[data-test*='error']"));
            Assert.That(errorMessage.Text.Contains("Username and password do not match")); 
        }
    }
}




