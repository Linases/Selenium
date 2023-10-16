using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.Xml.Linq;
using System.Drawing;
using OpenQA.Selenium.Chrome;

namespace Browser_Actions
{
    [TestFixture]
    public class Browser_ActionTests : BaseTest_BA
    {
        [Test]
        public void OpenNewWindowHandleTabs()
        {
            Setup();
            string originalWindow = Driver.CurrentWindowHandle;
            Assert.AreEqual(Driver.WindowHandles.Count, 1);

            var elementMultipleWindows = Driver.FindElement(By.CssSelector("[href*='windows']"));
            elementMultipleWindows.Click();
            var elementClickHere = Driver.FindElement(By.CssSelector("[href*='windows']"));
            elementClickHere.Click();
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(wd => wd.WindowHandles.Count == 2);
            foreach (string window in Driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    Driver.SwitchTo().Window(window);
                    break;
                }
            }
            wait.Until(wd => wd.Url == $"{MainUrl}/windows/new");
            var elementContent = Driver.FindElement(By.ClassName("example"));
            Assert.That(elementContent.Displayed);
        }

        [Test]
        public void NavigateBackForward()
        {
            Setup();
            var elementFormAuthentication = Driver.FindElement(By.CssSelector("[href*='login']"));
            elementFormAuthentication.Click();
            var elementUsername = Driver.FindElement(By.Id("username"));
            elementUsername.SendKeys("tomsmith");
            var elemenPassword = Driver.FindElement(By.Id("password"));
            elemenPassword.SendKeys("SuperSecretPassword!");
            var elementLoginButton = Driver.FindElement(By.ClassName("radius"));
            elementLoginButton.Click();
            var elementLogoutButton = Driver.FindElement(By.CssSelector("[href*='logout']"));
            elementLogoutButton.Click();
            Driver.Navigate().Back();
            Driver.Navigate().Forward();
            var pageUrl = Driver.Url;
            Assert.That(pageUrl, Is.EqualTo($"{MainUrl}/login"));
            var elementContentLogin = Driver.FindElement(By.Id("content"));
            Assert.That(elementContentLogin.Text.Contains("Login Page"));
        }

        [Test]
        public void NavigateToUrlAndRefresh()
        {
            Setup();
            var elementDynamicLoading = Driver.FindElement(By.CssSelector("[href*='dynamic_loading']"));
            elementDynamicLoading.Click();
            var elementExample = Driver.FindElement(By.CssSelector("[href='/dynamic_loading/1']"));
            elementExample.Click();
            var elementStartButton = Driver.FindElement(By.TagName("button"));
            elementStartButton.Click();

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finish"))).Displayed);
            var currentUrl = Driver.Url;
            Setup();
            var homePageUrl = Driver.Url;

            Assert.False(homePageUrl.Equals(currentUrl));
            Assert.That(homePageUrl, Is.EqualTo($"{MainUrl}/"));
            Driver.Navigate().Refresh();
        }

        [Test]
        public void MaximizeWindowChangeWindowSize()
        {
            Setup();
            var elementLargeDeepDom = Driver.FindElement(By.CssSelector("[href*='large']"));
            elementLargeDeepDom.Click();
            var elementLastDom = Driver.FindElement(By.XPath("//*[@id=\"large-table\"]/tbody/tr[50]/td[50]"));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView();", elementLastDom);
            Driver.Manage().Window.Maximize();
           
            var elementFirstLine = Driver.FindElement(By.XPath("//*[@id=\"content\"]/div/h3"));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView();", elementFirstLine);
            Assert.That(elementFirstLine.Displayed);
           
            var newSize = new Size(1000, 800);
            Driver.Manage().Window.Size = newSize;
            var currentSize = Driver.Manage().Window.Size;
            Assert.That(currentSize.Width, Is.EqualTo(newSize.Width));
            Assert.That(currentSize.Height, Is.EqualTo(newSize.Height));
        }

        [Test]
        public void HeadlessMode() 
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            Setup();
            var elementCheckboxes = Driver.FindElement(By.CssSelector("[href*='checkboxes']"));
            elementCheckboxes.Click();
            var elementChecked = Driver.FindElement(By.XPath("//*[@id='checkboxes']/input[1]"));
            elementChecked.Click();
            Assert.That(elementChecked.Selected, Is.True);
            var elementUnchecked = Driver.FindElement(By.XPath("//*[@id=\"checkboxes\"]/input[2]"));
            elementUnchecked.Click();
            Assert.That(elementUnchecked.Selected, Is.False);
        }
    }
}
