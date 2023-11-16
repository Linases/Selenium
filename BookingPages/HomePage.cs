using Apache.NMS;
using OpenQA.Selenium;
using Utilities;

namespace Booking_Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private IWebElement AttractionsLink => _driver.StaleElementExceptionWait(By.Id("attractions"),20,500);
        private IWebElement CarRentalsLink => _driver.FindElement(By.Id(("cars")));
        private IWebElement FlightsLink => _driver.FindElement(By.Id(("flights")));
       

        private IWebElement DeclineButton => _driver.WaitForElementVisible(By.XPath("//button[text()='Decline']"), 20);
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

   

        public void DeclineCookies() => DeclineButton.Click();
    }
    
}