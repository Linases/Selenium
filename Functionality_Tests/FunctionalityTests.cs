using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.DevTools.V115.Page;

namespace Functionality_Tests_Suit
{
    [TestFixture]
    public class FunctionalityTests : BaseTest
    {
        [Test, Order(1)]
        public void PriceSorting()
        {
            SuccessfulLogin();
            var elementItemSorting = driver.FindElement(By.TagName("select"));
            elementItemSorting.Click();
            var elementPriceSorting = driver.FindElement(By.CssSelector("[value='lohi']"));
            elementPriceSorting.Click();
            var elementMinPrice = driver.FindElement(By.CssSelector("[class='inventory_list']:first-child"));
            Assert.That(elementMinPrice.Text.Contains("7.99"));

            var elementItemsList = driver.FindElements(By.ClassName("inventory_list"));
            Assert.That(elementItemsList, Is.Ordered.Ascending);
        }

        [Test, Order(2)]
        public void RemoveFromCart()
        {
            AddProductToCart();
            var elementShopingCartIcon = driver.FindElement(By.ClassName("shopping_cart_link"));
            elementShopingCartIcon.Click();
            var elementAddedItem = driver.FindElement(By.ClassName("shopping_cart_badge"));
            Assert.That(elementAddedItem.Text.Contains('1'));
            var elementRemoveButton = driver.FindElement(By.XPath("//button[text() = 'Remove']"));
            elementRemoveButton.Click();
            var elementAddedItem2 = driver.FindElements(By.ClassName("shopping_cart_badge"));
            Assert.That(elementAddedItem2, Is.Empty);
        }

        [Test, Order(3)]
        public void Logout()
        {
            SuccessfulLogin();
            var elementMeniuButton = driver.FindElement(By.Id("react-burger-menu-btn"));
            elementMeniuButton.Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("logout_sidebar_link"))).Click();
            var pageUrl = driver.Url;
            Assert.That(pageUrl, Is.EqualTo(mainUrl));
            var elementLoginButton = driver.FindElement(By.Id("login-button"));
            Assert.That(elementLoginButton.Displayed);
        }
    }
}
