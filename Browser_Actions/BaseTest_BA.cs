using Functionality_Tests_Suit.Constants;
using Functionality_Tests_Suit.FactoryPattern;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Browser_Actions
{
    public class BaseTest_BA
    {
        protected static IWebDriver Driver;
        protected readonly string MainUrl;

        public BaseTest_BA()
        {
            MainUrl = "http://the-internet.herokuapp.com";
        }

        [OneTimeSetUp]
        public static void OneTimeSetUp()
        {
            Driver = BrowserFactory.GetDriver(BrowserType.Firefox);
        }

        public void Setup()
        {
            Driver.Navigate().GoToUrl(MainUrl);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            BrowserFactory.CloseDriver();
        }
    }
}