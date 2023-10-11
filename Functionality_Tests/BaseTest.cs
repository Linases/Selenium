using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace Functionality_Tests_Suit
{
    public class BaseTest
    {
        public IWebDriver driver;
        public string mainUrl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
            mainUrl = "https://www.saucedemo.com/";
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
        }

        public void SuccessfulLogin()
        {
            driver.Navigate().GoToUrl(mainUrl);
            var elementUserName = driver.FindElement(By.Id("user-name"));
            elementUserName.SendKeys("standard_user");
            var elementPassword = driver.FindElement(By.Id("password"));
            elementPassword.SendKeys("secret_sauce");
            var elementLoginButton = driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();
            var pageUrl = driver.Url;
            Assert.That(pageUrl, Is.EqualTo($"{mainUrl}inventory.html"));
        }

        public void AddProductToCart()
        {
            SuccessfulLogin();
            var elementItem = driver.FindElement(By.Id("item_4_title_link"));
            elementItem.Click();
            var elementAddButton = driver.FindElement(By.XPath("//button[text() = 'Add to cart']"));
            elementAddButton.Click();
            var elementAddedItem = driver.FindElement(By.ClassName("shopping_cart_badge"));
            Assert.That(elementAddedItem.Text.Contains('1'));
        }
    }
}
