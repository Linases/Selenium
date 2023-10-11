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
using Functionality_Tests_Suit;

namespace Selenium
{
    [TestFixture]
    public class SeleniumTests: BaseTest
    {
        [Test, Order(1)]
        public void SuccessfullLoginAndItemOpening()
        {
            SuccessfulLogin();
            var elementItem = Driver.FindElement(By.Id("item_4_title_link"));
            elementItem.Click();
            var itemUrl = Driver.Url;
            Assert.That(itemUrl, Is.EqualTo($"{MainUrl}inventory-item.html?id=4"));

            var itemTitle = Driver.FindElement(By.CssSelector("[class*='inventory_details_name']"));
            Assert.That(itemTitle.Text.Contains("Sauce Labs Backpack"));

            var itemDescription = Driver.FindElement(By.CssSelector("[class*='inventory_details_desc']"));
            Assert.That(itemDescription.Text.Contains("streamlined Sly Pack"));

            var itemPrice = Driver.FindElement(By.ClassName("inventory_details_price"));
            Assert.That(itemPrice.Text.Contains("$29.99"));
        }

        [Test, Order(2)]
        public void FailedLoginWithEmptyCredentials()
        {
            var elementUserName = Driver.FindElement(By.Id("user-name"));
            Assert.That(elementUserName.GetAttribute("value"), Is.Empty);
            var elementPassword = Driver.FindElement(By.Id("password"));
            Assert.That(elementPassword.GetAttribute("value"), Is.Empty);
            var elementLoginButton = Driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();
            var errorMessage = Driver.FindElement(By.CssSelector("[data-test*='error']"));

            Assert.That(errorMessage.Displayed);
            Assert.That(errorMessage.Text.Contains("Username is required"));

            CheckErrorIcon(elementUserName);
            CheckErrorIcon(elementPassword);
        }

        [Test, Order(3)]
        public void FailedLoginWithInvalidCredentials()
        {
            var elementUserName = Driver.FindElement(By.Id("user-name"));
            elementUserName.SendKeys("invalid_user");
            var elementPassword = Driver.FindElement(By.Id("password"));
            elementPassword.SendKeys("invalid_password");
            var elementLoginButton = Driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();

            var errorMessage = Driver.FindElement(By.CssSelector("[data-test*='error']"));
            Assert.That(errorMessage.Displayed);
            Assert.That(errorMessage.Text.Contains("Username and password do not match"));

            CheckErrorIcon(elementUserName);
            CheckErrorIcon(elementPassword);
        }

        private void CheckErrorIcon(IWebElement webElement)
        {
            var parentElement = webElement.FindElement(By.XPath(".."));
            var errorMarkIcon = parentElement.FindElement(By.CssSelector("[class*='svg-inline--fa']"));
            Assert.That(errorMarkIcon.Displayed);
        }
    }
}
