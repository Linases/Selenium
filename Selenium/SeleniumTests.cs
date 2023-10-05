using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System;

namespace Selenium
{
    [TestFixture]
    public class SeleniumTests
    {
        private IWebDriver _driver;
        private string _mainUrl;

        [OneTimeSetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _mainUrl = "https://www.saucedemo.com/";
            _driver.Navigate().GoToUrl(_mainUrl);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [Test, Order(1)]
        public void SuccessfullLoginAndItemOpening()
        {
            _driver.Navigate().GoToUrl(_mainUrl);
            var elementUserName = _driver.FindElement(By.Id("user-name"));
            elementUserName.SendKeys("standard_user");
            var elementPassword = _driver.FindElement(By.Id("password"));
            elementPassword.SendKeys("secret_sauce");

            var elementLoginButton = _driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();

            var pageUrl = _driver.Url;
            Assert.That(pageUrl, Is.EqualTo("https://www.saucedemo.com/inventory.html"));
            string pageTitle = _driver.Title;
            Assert.That(pageTitle, Is.EqualTo("Swag Labs"));

            var elementItem = _driver.FindElement(By.Id("item_4_title_link"));
            elementItem.Click();

            var itemUrl = _driver.Url;
            Assert.That(itemUrl, Is.EqualTo("https://www.saucedemo.com/inventory-item.html?id=4"));

            var itemTitle = _driver.FindElement(By.CssSelector("[class*='inventory_details_name']"));
            Assert.That(itemTitle.Text.Contains("Sauce Labs Backpack"));

            var itemDescription = _driver.FindElement(By.CssSelector("[class*='inventory_details_desc']"));
            Assert.That(itemDescription.Text.Contains("streamlined Sly Pack"));

            var itemPrice = _driver.FindElement(By.ClassName("inventory_details_price"));
            Assert.That(itemPrice.Text.Contains("$29.99"));
        }

        [Test, Order(2)]
        public void FailedLoginWithEmptyCredentials()
        {
            _driver.Navigate().GoToUrl(_mainUrl);
            var elementUserName = _driver.FindElement(By.Id("user-name"));
            Assert.That(elementUserName.GetAttribute("value"), Is.Empty);
            var elementPassword = _driver.FindElement(By.Id("password"));
            Assert.That(elementPassword.GetAttribute("value"), Is.Empty);
            var elementLoginButton = _driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();
            var errorMessage = _driver.FindElement(By.CssSelector("[data-test*='error']"));
            Assert.That(errorMessage.Displayed);
            Assert.That(errorMessage.Text.Contains("Username is required"));

            CheckErrorIcon(elementUserName);
            CheckErrorIcon(elementPassword);
            
        }

        [Test, Order(3)]
        public void FailedLoginWithInvalidCredentials()
        {
            _driver.Navigate().GoToUrl(_mainUrl);
            var elementUserName = _driver.FindElement(By.Id("user-name"));
            elementUserName.SendKeys("invalid_user");
            var elementPassword = _driver.FindElement(By.Id("password"));
            elementPassword.SendKeys("invalid_password");
            var elementLoginButton = _driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();
            
            var errorMessage = _driver.FindElement(By.CssSelector("[data-test*='error']"));
            Assert.That(errorMessage.Displayed);
            Assert.That(errorMessage.Text.Contains("Username and password do not match"));

            CheckErrorIcon(elementUserName);
            CheckErrorIcon(elementPassword);
        }
        private void CheckErrorIcon (IWebElement webElement)
        {
            var parentElement = webElement.FindElement(By.XPath(".."));
            var errorMarkIcon = parentElement.FindElement(By.CssSelector("[class*='svg-inline--fa']"));
            Assert.That(errorMarkIcon.Displayed);
        }
    }
}




