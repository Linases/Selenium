using Apache.NMS;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Utilities;

namespace Booking_Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private By DissmissGeniusAlert => (By.CssSelector(".c0528ecc22 button"));
        private IWebElement AttractionsLink => _driver.FindElement(By.Id("attractions"));
        private IWebElement CarRentalsLink => _driver.FindElement(By.Id("cars"));
        private IWebElement FlightsLink => _driver.FindElement(By.Id("flights"));
        private IWebElement AirportTaxi => _driver.FindElement(By.Id("airport_taxis"));
        private IWebElement FlightsHeader => _driver.FindElement(By.CssSelector(".title-wrapper h1"));

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            if (!driver.Title.Contains("Booking.com"))
            {
                throw new IllegalStateException("This is not The Internet Page," + " current page is: " + driver.Url);
            }
        }

        public void OpenAttractionsPage() => AttractionsLink.Click();

        public void OpenCarRentalsPage() => CarRentalsLink.Click();

        public void OpenFlightsPage() => FlightsLink.Click();

        public void OpenAirportTaxiPage() => AirportTaxi.Click();

        public void DissmissAlert()
        {
            try
            {
                var dismissAlert = WebDriverExtensions.GetWait(_driver, 5, 200).Until(ExpectedConditions.ElementIsVisible(DissmissGeniusAlert));

                if (dismissAlert.Displayed)
                {
                    dismissAlert.Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Alert dismissal element did not appear within the specified time.");
            }
        }

        public void DeclineCookies()
        {
            var declineButton = _driver.GetWaitForElementClicable(By.XPath("//button[text()='Decline']"));
            declineButton.Click();
        }

        public string GetFlightsHeader() => FlightsHeader.Text;
    }
}
