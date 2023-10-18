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

        private (string userName, string userPassword) standartUserCredentials = ("standard_user", "secret_sauce");
        private (string userName, string userPassword) invalidUserCredentials = ("invalid_user", "invalid_password");

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

        public void StandardUserLogin()
        {
            Login(standartUserCredentials.userName, standartUserCredentials.userPassword);
            var pageUrl = Driver.Url;
            Assert.That(pageUrl, Is.EqualTo($"{MainUrl}/inventory.html"));
            var pageTitle = Driver.Title;
            Assert.That(pageTitle, Is.EqualTo("Swag Labs"));
        }
        public void InvalidUserLogin()
        {
            Login(invalidUserCredentials.userName, invalidUserCredentials.userPassword);
        }

        private void Login(string username, string password)
        {
            var elementUserName = Driver.FindElement(By.Id("user-name"));
            elementUserName.SendKeys($"{username}");
            var elementPassword = Driver.FindElement(By.Id("password"));
            elementPassword.SendKeys($"{password}");
            var elementLoginButton = Driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();
        }

        public void AddProductToCart()
        {
            var elementItem = Driver.FindElement(By.Id("item_4_title_link")).Click;
            var elementAddButton = Driver.FindElement(By.XPath("//button[text() = 'Add to cart']"));
            elementAddButton.Click();
            var elementAddedItem = Driver.FindElement(By.ClassName("shopping_cart_badge"));
            Assert.That(elementAddedItem.Text.Contains('1'));
        }
    }
}
