using Apache.NMS;
using OpenQA.Selenium;
using Wrappers;

namespace Booking_Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private By DissmissCookies => By.XPath("//button[text()='Decline']");
        private By DissmissGeniusAlert => (By.CssSelector(".c0528ecc22 button"));
        private Button DissmissGeniusAlertButton => new Button(DissmissGeniusAlert);
        private Button DissmissCookiesAlertButton => new Button(DissmissCookies);
        private Button AttractionsLink => new Button(_driver.FindElement(By.Id("attractions")));
        private Button CarRentalsLink => new Button(_driver.FindElement(By.Id("cars")));
        private Button FlightsLink => new Button(_driver.FindElement(By.Id("flights")));
        private Button AirportTaxi => new Button(_driver.FindElement(By.Id("airport_taxis")));

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

        public void DissmissAlert() => DissmissGeniusAlertButton.ClickIfDisplayed(DissmissGeniusAlert);

        public void DeclineCookies() => DissmissCookiesAlertButton.ClickIfDisplayed(DissmissCookies);
    }
}
