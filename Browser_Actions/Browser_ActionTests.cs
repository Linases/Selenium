using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing;
using Functionality_Tests_Suit.Constants;
using Functionality_Tests_Suit.FactoryPattern;
using OpenQA.Selenium.Chrome;

namespace Browser_Actions
{
    [TestFixture]
    public class Browser_ActionTests
    {
        private static IWebDriver _driver;
        private readonly string _mainUrl;

        public Browser_ActionTests()
        {
            _mainUrl = "http://the-internet.herokuapp.com";
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
           _driver = BrowserFactory.GetDriver(BrowserType.Chrome);
        }

        [SetUp]
        public void Setup()
        {
            _driver.Navigate().GoToUrl(_mainUrl);
        }

        [Test]
        public void OpenNewWindowHandleTabs()
        {
            var originalWindow = _driver.CurrentWindowHandle;
            Assert.That(_driver.WindowHandles.Count, Is.EqualTo(1), "Opened window is not the first");
            var elementMultipleWindows = _driver.FindElement(By.CssSelector("[href*='windows']"));
            elementMultipleWindows.Click();
            var elementClickHere = _driver.FindElement(By.XPath("//*[@class='example']//a"));
            elementClickHere.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(wd => wd.WindowHandles.Count == 2);
            foreach (var window in _driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    _driver.SwitchTo().Window(window);
                    break;
                }
            }
            wait.Until(wd => wd.Url == $"{_mainUrl}/windows/new");
            var elementContent = _driver.FindElement(By.ClassName("example"));
            Assert.That(elementContent.Displayed, " Content 'New Window' is not displayed");
            _driver.Close();
        }

        [Test]
        public void NavigateBackForward()
        {
            var elementFormAuthentication = _driver.FindElement(By.CssSelector("[href*='login']"));
            elementFormAuthentication.Click();
            var elementUsername = _driver.FindElement(By.Id("username"));
            elementUsername.SendKeys("tomsmith");
            var elemenPassword = _driver.FindElement(By.Id("password"));
            elemenPassword.SendKeys("SuperSecretPassword!");
            var elementLoginButton = _driver.FindElement(By.XPath("//*[@class='radius']"));
            elementLoginButton.Click();
            var elementLogoutButton = _driver.FindElement(By.CssSelector("[href*='logout']"));
            elementLogoutButton.Click();
            _driver.Navigate().Back();
            var pageUrlBack = _driver.Url;
            Assert.That(pageUrlBack, Is.EqualTo($"{_mainUrl}/secure"));
            _driver.Navigate().Forward();
            var pageUrlForward = _driver.Url;
            Assert.That(pageUrlForward, Is.EqualTo($"{_mainUrl}/login"));
            var elementContentLogin = _driver.FindElement(By.Id("content"));
            Assert.That(elementContentLogin.Text.Contains("Login Page"));
        }

        [Test]
        public void NavigateToUrlAndRefresh()
        {
            var elementDynamicLoading = _driver.FindElement(By.CssSelector("[href*='dynamic_loading']"));
            elementDynamicLoading.Click();
            var elementExample = _driver.FindElement(By.XPath("//*[@id='content']//a"));
            elementExample.Click();
            var elementStartButton = _driver.FindElement(By.TagName("button"));
            elementStartButton.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.Id("finish"))).Displayed);
            var currentUrl = _driver.Url;
            _driver.Navigate().GoToUrl(_mainUrl);
            var homePageUrl = _driver.Url;

            Assert.False(homePageUrl.Equals(currentUrl));
            Assert.That(homePageUrl, Is.EqualTo($"{_mainUrl}/"));
            _driver.Navigate().Refresh();
        }

        [Test]
        public void MaximizeWindowChangeWindowSize()
        {
            var elementLargeDeepDom = _driver.FindElement(By.CssSelector("[href*='large']"));
            elementLargeDeepDom.Click();

            var elementLastDom = _driver.FindElement(By.XPath("//*[@id='large-table']//tr[50]/td[50]"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", elementLastDom);
            Assert.That(elementLastDom.Displayed, "Last DOM '50.50' is not displayed");
            _driver.Manage().Window.Maximize();

            var elementFirstLine = _driver.FindElement(By.XPath("//*[@id='content']/div/h3"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView();", elementFirstLine);
            Assert.That(elementFirstLine.Displayed, "First Line 'Large & Deep DOM' is not displayed");

            var newSize = new Size(1000, 800);
            _driver.Manage().Window.Size = newSize;
            var currentSize = _driver.Manage().Window.Size;
            Assert.That(currentSize.Width, Is.EqualTo(newSize.Width), "Current window width is not 1000");
            Assert.That(currentSize.Height, Is.EqualTo(newSize.Height), "Current window height is not 800");
        }

        [Test]
        public void HeadlessMode()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless=new");
            using var headlessDriver = new ChromeDriver(options);

            headlessDriver.Navigate().GoToUrl(_mainUrl);
            var elementCheckboxes = headlessDriver.FindElement(By.CssSelector("[href*='checkboxes']"));
            elementCheckboxes.Click();
            var elementChecked = headlessDriver.FindElement(By.XPath("//*[@id='checkboxes']/input[1]"));
            elementChecked.Click();
            Assert.That(elementChecked.Selected, Is.True, "checkbox 1 is not selected");
            var elementUnchecked = headlessDriver.FindElement(By.XPath("//*[@id='checkboxes']/input[2]"));
            elementUnchecked.Click();
            Assert.That(elementUnchecked.Selected, Is.False, "checkbox 2 is selected");
            headlessDriver.Close();
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            BrowserFactory.CloseDriver();
        }
    }
}
