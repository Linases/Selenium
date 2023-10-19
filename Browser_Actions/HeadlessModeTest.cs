using Functionality_Tests_Suit.FactoryPattern;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Browser_Actions
{
    [TestFixture]
    internal class HeadlessModeTest
    {
        private static IWebDriver _driver;
        private readonly string _mainUrl;

        public HeadlessModeTest()
        {
            _mainUrl = "http://the-internet.herokuapp.com";
        }

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            _driver = new ChromeDriver(options);
            _driver.Navigate().GoToUrl(_mainUrl);
        }

        [Test]
        public void HeadlessMode()
        {
            var elementCheckboxes = _driver.FindElement(By.CssSelector("[href*='checkboxes']"));
            elementCheckboxes.Click();
            var elementChecked = _driver.FindElement(By.XPath("//*[@id='checkboxes']/input[1]"));
            elementChecked.Click();
            Assert.That(elementChecked.Selected, Is.True, "checkbox 1 is not selected");
            var elementUnchecked = _driver.FindElement(By.XPath("//*[@id='checkboxes']/input[2]"));
            elementUnchecked.Click();
            Assert.That(elementUnchecked.Selected, Is.False, "checkbox 2 is selected");
        }

        [TearDown]
        public static void TearDown()
        {
            BrowserFactory.CloseDriver();
        }
    }
}
