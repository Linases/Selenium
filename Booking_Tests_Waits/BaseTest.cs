using Booking_Pages;
using Functionality_Tests_Suit.Constants;
using Functionality_Tests_Suit.FactoryPattern;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Booking_Tests_Waits
{
    public class BaseTest
    {
        protected static IWebDriver Driver;
        protected readonly string MainUrl;
        private HomePage _homePage;
    
        public BaseTest()
        {
            MainUrl = "https://www.booking.com";
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Driver = BrowserFactory.GetDriver(BrowserType.Chrome);
            Driver.Navigate().GoToUrl(MainUrl);
            Assert.That(Driver.Url, Is.EqualTo($"{MainUrl}/"), "Homepage is not displayed");
            _homePage = new HomePage(Driver);
            _homePage.DeclineCookies();
        }

        [OneTimeTearDown]
        public static void OneTimeTearDown()
        {
            BrowserFactory.CloseDriver();
        }
    }
}