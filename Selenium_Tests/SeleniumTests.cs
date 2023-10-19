using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using Functionality_Tests_Suit;
using OpenQA.Selenium.Interactions;

namespace Selenium
{
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable(scope: ParallelScope.Self)]
    public class SeleniumTests : BaseTest
    {
        [Test, Order(1)]
        public void SuccessfullLoginAndItemOpening()
        {
            StandardUserLogin();
            IWebElement element = Driver.FindElement(By.XPath("//*[@id='item_4_title_link']/div"));
            Actions act = new Actions(Driver);
            act.MoveToElement(element).Click().Perform();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var itemUrl = Driver.Url;
            Assert.That(itemUrl, Is.EqualTo($"{MainUrl}/inventory-item.html?id=4"));

            var itemTitle = Driver.FindElement(By.CssSelector("[class*='inventory_details_name']"));
            Assert.That(itemTitle.Text.Contains("Sauce Labs Backpack"), "'Sauce Labs Backpack' is not found in the context");

            var itemDescription = Driver.FindElement(By.CssSelector("[class*='inventory_details_desc']"));
            Assert.That(itemDescription.Text.Contains("streamlined Sly Pack"));

            var itemPrice = Driver.FindElement(By.ClassName("inventory_details_price"));
            Assert.That(itemPrice.Text.Contains("$29.99"), "Item price is not $29.99");
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
            InvalidUserLogin();
            var elementUserName = Driver.FindElement(By.Id("user-name"));
            var elementPassword = Driver.FindElement(By.Id("password"));

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
