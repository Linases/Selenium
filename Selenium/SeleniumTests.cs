using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System.Linq;

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
        [Test, Order(1)]
        public void SuccessfullLoginAndItemOpening()
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
            string pageTitle = driver.Title;
            Assert.That(pageTitle, Is.EqualTo("Swag Labs"));

            var elementItem = driver.FindElement(By.Id("item_4_title_link"));
            elementItem.Click();

            string itemUrl = driver.Url;
            Assert.That(itemUrl, Is.EqualTo("https://www.saucedemo.com/inventory-item.html?id=4"));

            var itemTitle = driver.FindElement(By.CssSelector("[class*='inventory_details_name']"));
            Assert.That(itemTitle.Text.Contains("Sauce Labs Backpack"));

            var itemDescription = driver.FindElement(By.CssSelector("[class*='inventory_details_desc']"));
            Assert.That(itemDescription.Text.Contains("streamlined Sly Pack"));

            var itemPrice = driver.FindElement(By.ClassName("inventory_details_price"));
            Assert.That(itemDescription.Text.Contains("$29.99"));
        }
        [Test, Order(2)]
        public void FailedLoginWithEmptyCredentials()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var elementUserName = driver.FindElement(By.Id("user-name"));
            var elementPassword = driver.FindElement(By.Id("password"));
            var elementLoginButton = driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();
            var errorMessage = driver.FindElement(By.CssSelector("[data-test*='error']"));
            Assert.That(errorMessage.Text.Contains("Username is required"));
           
            var errorMarkUserName = driver.FindElement(By.CssSelector("[class*=svg-inline--fa]"));
            Assert.That(errorMarkUserName.Displayed);
           
            var errorMarkPassword = driver.FindElement(By.CssSelector("[class*=svg-inline--fa]"));
            Assert.That(errorMarkPassword.Displayed);
        }
        [Test, Order(3)]
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
            
            var errorMarkUserName = driver.FindElement(By.CssSelector("[class*=svg-inline--fa]"));
            Assert.That(errorMarkUserName.Displayed);
           
            var errorMarkPassword = driver.FindElement(By.CssSelector("[class*=svg-inline--fa]"));
            Assert.That(errorMarkPassword.Displayed);
        }
    }
}




