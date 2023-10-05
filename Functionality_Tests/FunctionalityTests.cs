using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;


namespace Functionality_Tests_Suit
{
    [TestFixture]  
    
    public class FunctionalityTests: BaseTest
    {
 
        [Test, Order (1)]
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
        [Test, Order (2)]

        public void AddToCart()
        {
            AddProductToCart();
        }
        [Test, Order (3)]
        public void RemoveFromCart()
        {
            AddProductToCart();
            var elementShopingcartIcon = driver.FindElement(By.ClassName("shopping_cart_link"));
            elementShopingcartIcon.Click();
            var elementRemoveButton = driver.FindElement(By.XPath("//button[text() = 'Remove']"));
            elementRemoveButton.Click();
            var elementShopingcartIcon2 = driver.FindElement(By.ClassName("shopping_cart_link"));
            Assert.That(elementShopingcartIcon2.Text.Contains('1'), Is.False);
        }
    }
}
