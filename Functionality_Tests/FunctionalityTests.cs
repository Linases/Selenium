using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Functionality_Tests_Suit
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(scope: ParallelScope.Self)]
    public class FunctionalityTests : BaseTest
    {
        private bool needToRemoveFromTheCart;
        private List<string> itemsToRemove = new List<string>();

        [SetUp]
        public void SetUp()
        {
            StandardUserLogin();
        }

        [Test]
        public void PriceSorting()
        {
            var selectElement = Driver.FindElement(By.TagName("select"));
            selectElement.Click();
            var select = new SelectElement(selectElement);
            select.SelectByValue("lohi");

            var elementMinPrice = Driver.FindElement(By.CssSelector("[class='inventory_list']:first-child"));
            Assert.That(elementMinPrice.Text.Contains("7.99"), "Minimal price is not $7.99");

            var elementItemsList = Driver.FindElements(By.ClassName("inventory_list"));
            Assert.That(elementItemsList, Is.Ordered.Ascending, "List of items is not ordered in ascending order");
        }

        [Test]
        public void AddToCart()
        {
            AddProductToCart();
            needToRemoveFromTheCart = true;
            itemsToRemove.Add("1");
        }

        [Test]
        public void AddAndRemoveFromCart()
        {
            AddProductToCart();
            needToRemoveFromTheCart = true;
            RemoveItemFromCart();
        }

        public void RemoveItemFromCart()
        {
            var elementShopingCartIcon = Driver.FindElement(By.ClassName("shopping_cart_link"));
            elementShopingCartIcon.Click();
            var elementAddedItem = Driver.FindElement(By.ClassName("shopping_cart_badge"));
            Assert.That(elementAddedItem.Text.Contains('1'), "Added item in a cart is not 1");
            var elementRemoveButton = Driver.FindElement(By.XPath("//button[text() = 'Remove']"));
            if (elementRemoveButton.Displayed)
            {
                elementRemoveButton.Click();
            }
            var elementAddedItem2 = Driver.FindElements(By.ClassName("shopping_cart_badge"));
            Assert.That(elementAddedItem2, Is.Empty, "Shopping cart is not empty");
        }

        [Test]
        public void Logout()
        {
            var elementMeniuButton = Driver.FindElement(By.Id("react-burger-menu-btn"));
            elementMeniuButton.Click();
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("logout_sidebar_link"))).Click();
            var pageUrl = Driver.Url;
            Assert.That(pageUrl, Is.EqualTo($"{MainUrl}/"));
            var elementLoginButton = Driver.FindElement(By.Id("login-button"));
            Assert.That(elementLoginButton.Displayed);
        }

        [TearDown]
        public void TearDown()
        {
            if (needToRemoveFromTheCart)
            {
                foreach (var item in itemsToRemove)
                {
                    RemoveItemFromCart();
                }
                itemsToRemove.Clear();
            }
            needToRemoveFromTheCart = false;
        }
    }
}
