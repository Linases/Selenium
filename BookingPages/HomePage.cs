using Apache.NMS;
using OpenQA.Selenium;
using Utilities;

namespace Booking_Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private IWebElement AttractionsLink => _driver.WaitForElementClicable(By.Id("attractions"));
        private IWebElement CarRentalsLink => _driver.WaitForElementClicable(By.Id("cars"));
        private IWebElement FlightsLink => _driver.WaitForElementClicable(By.Id("flights"));
        private IWebElement AirportTaxi => _driver.WaitForElementClicable(By.Id("airport_taxis"));

        private IWebElement DeclineButton => _driver.WaitForElementVisible(By.XPath("//button[text()='Decline']"));
        private  IWebElement DissmissGeniusAlert => _driver.WaitForElementClicable(By.CssSelector(".c0528ecc22 button"));
        private  IWebElement AlertBox => _driver.FindElement(By.CssSelector(".c0528ecc22"));
        
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
            if (AlertBox.Displayed)
            {
                DissmissGeniusAlert.Click();
            }

        }


        public void DeclineCookies() => DeclineButton.Click();
    }
    
}