using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using Functionality_Tests_Suit.FactoryPattern;
using Functionality_Tests_Suit.Constants;

namespace Functionality_Tests_Suit
{
    public class BaseTest
    {
        protected static IWebDriver Driver;
        protected readonly string MainUrl;

        public BaseTest()
        {
            MainUrl = "https://www.saucedemo.com";
        }

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            Driver = BrowserFactory.GetDriver(BrowserType.Firefox);
        }

        [SetUp]
        public void Setup()
        {
            Driver.Navigate().GoToUrl(MainUrl);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            BrowserFactory.CloseDriver();
        }

        public void SuccessfulLogin()
        {
            var elementUserName = Driver.FindElement(By.Id("user-name"));
            elementUserName.SendKeys("standard_user");
            var elementPassword = Driver.FindElement(By.Id("password"));
            elementPassword.SendKeys("secret_sauce");
            var elementLoginButton = Driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();
            var pageUrl = Driver.Url;
            Assert.That(pageUrl, Is.EqualTo($"{MainUrl}/inventory.html"));
            var pageTitle = Driver.Title;
            Assert.That(pageTitle, Is.EqualTo("Swag Labs"));
        }

        public void AddProductToCart()
        {
            SuccessfulLogin();
            var elementItem = Driver.FindElement(By.Id("item_4_title_link")).Click;
            var elementAddButton = Driver.FindElement(By.XPath("//button[text() = 'Add to cart']"));
            elementAddButton.Click();
            var elementAddedItem = Driver.FindElement(By.ClassName("shopping_cart_badge"));
            Assert.That(elementAddedItem.Text.Contains('1'));
        }
    }
}
