using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium
{
    public class SeleniumTests
    {
        ChromeDriver driver = new ChromeDriver();

        [Test]
        public void SuccessfulLogin()
        {

            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            IWebElement elementUserName = driver.FindElement(By.Id("user-name"));
            elementUserName.SendKeys("standard_user");
            IWebElement elementPassword = driver.FindElement(By.Id("password"));
            elementPassword.SendKeys("secret_sauce");

            IWebElement elementLoginButton = driver.FindElement(By.Id("login-button"));
            elementLoginButton.Click();

            string pageUrl = driver.Url;
            Assert.That(pageUrl, Is.EqualTo("https://www.saucedemo.com/inventory.html"));

            IWebElement elementItem = driver.FindElement(By.Id("item_4_title_link"));
            elementItem.Click();

            string itemUrl = driver.Url;
            Assert.That(itemUrl, Is.EqualTo("https://www.saucedemo.com/inventory-item.html?id=4"));

            driver.Quit();
        }
    }
}

