using Apache.NMS;
using OpenQA.Selenium;
using Utilities;

namespace Booking_Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private IWebElement AttractionsLink => _driver.FindElement(By.Id("attractions"));
        private IWebElement CarRentalsLink => _driver.FindElement(By.Id("cars"));
        private IWebElement FlightsLink => _driver.FindElement(By.Id("flights"));
        private IWebElement AirportTaxi => _driver.FindElement(By.Id("airport_taxis"));

        private IWebElement DeclineButton => _driver.FindElement(By.XPath("//button[text()='Decline']"));
        private IWebElement DissmissGeniusAlert => _driver.FindElement(By.CssSelector(".c0528ecc22 button"));

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

            _driver.NoSuchElementExceptionWait(DissmissGeniusAlert);
            if (DissmissGeniusAlert.Displayed)
            {
                DissmissGeniusAlert.Click();
            }

        }

        public void DeclineCookies() => DeclineButton.Click();

        public string GetFlightsHeader() => FlightsHeader.Text;
    }


}