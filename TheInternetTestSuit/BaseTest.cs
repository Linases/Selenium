using Functionality_Tests_Suit.Constants;
using Functionality_Tests_Suit.FactoryPattern;
using OpenQA.Selenium;

namespace TheInternetTestSuit
{
    public class BaseTest
    {
        protected static IWebDriver Driver;
        protected readonly string MainUrl;

        public BaseTest()
        {
            MainUrl = "http://the-internet.herokuapp.com";
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Driver = BrowserFactory.GetDriver(BrowserType.Chrome);
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            BrowserFactory.CloseDriver();
        }
    }
}