using Apache.NMS;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BookingPages
{
    public class AirportTaxiPage
    {
        private readonly IWebDriver _driver;
        private IWebElement PickUpLocation => _driver.FindElement(By.Id("pickupLocation"));
        private IWebElement Destination => _driver.FindElement(By.Id("dropoffLocation"));
        private IWebElement PickUpDate => _driver.FindElement(By.XPath("//button[@data-test='rw-date-field__link--pickup']/span"));
        private IWebElement PickUpTime => _driver.FindElement(By.XPath("//button[@data-test='rw-time-field--pickup']/span"));
        private IWebElement SearchButton => _driver.FindElement(By.XPath("(//span[@data-test='button-content'])[1]"));

        private IWebElement Calendar => _driver.FindElement(By.XPath("//*[@data-test='rw-calendar']"));
        private IWebElement CurrentMonth => Calendar.FindElement(By.CssSelector(".rw-c-date-picker__calendar-caption"));
        private IWebElement NextMonthArrow => Calendar.FindElement(By.XPath("//*[@data-test='rw-date-picker__btn--next']"));
        //  By Auto_CompleteListPickUp => (By.CssSelector("#pickupLocation-items"));
        // By Auto_CompleteListPickUp => (By.CssSelector("#dropoffLocation-items"));
        private IList<IWebElement> CurrentDays => Calendar.FindElements(By.XPath("//*[@data-test='rw-calendar']//td"));
        private IList<IWebElement> SearchResultsList => _driver.FindElements(By.CssSelector(".SRM_527ba3f0"));

        private SelectElement SelectHour => new SelectElement(_driver.FindElement(By.CssSelector("#pickupHour")));
        private SelectElement SelectMinutes => new SelectElement(_driver.FindElement(By.CssSelector("#pickupMinute")));
        private IWebElement ConfirmTimeButton => _driver.FindElement(By.XPath("//*[@data-test='rw-time-picker__confirm-button']"));
        private IWebElement ContinueButton => _driver.FindElement(By.XPath("//button[@data-test='continue-action-bar__continue-button']"));
        private IWebElement ItinerarySummary => _driver.FindElement(By.XPath("//*[@data-testid='route-summary-wrapper']"));

        public AirportTaxiPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterPickUpLocation(string pickUpLocation)
        {
            PickUpLocation.SendKeys(pickUpLocation);
            PickUpLocation.SendKeys(Keys.Enter);
        }

        public void EnterDestinationLocation(string destination)
        {
            Destination.SendKeys(destination);
            Destination.SendKeys(Keys.Enter);
        }

        public string GetPickUpLocation() => PickUpLocation.Text; //no string in element

        public string GetDestination() => Destination.Text; //no string in element

        public void ClickDateField() => PickUpDate.Click();

        public void SelectDate(DateTime taxiDate)
        {
            var currentMonthYearText = CurrentMonth.Text;

            var desiredMonthYearText = taxiDate.ToString("MMMM yyyy");
            while (!currentMonthYearText.Contains(desiredMonthYearText))
            {
                NextMonthArrow.Click();
            }

            var desiredDayElement = CurrentDays.FirstOrDefault(element => element.Text.Contains($"{taxiDate.Day}"));
            desiredDayElement.Click();
        }

        public string GetSelectedDate() => PickUpDate.Text;

        public void ClickTimeField() => PickUpTime.Click();

        public void ConfirmTime() => ConfirmTimeButton.Click();

        public void SelectHourValue(string hour) => SelectHour.SelectByValue(hour);

        public void SelectMinutesValue(string minutes) => SelectMinutes.SelectByValue(minutes);

        public string GetPickUpTime() => PickUpTime.Text;

        public void ClickSearch() => SearchButton.Click();

        public void SelectTaxi()
        {
            var lastTaxi = SearchResultsList.LastOrDefault(x => x.Displayed);
            lastTaxi.Click();
        }

        public void ClickContinueButton() => ContinueButton.Click();

        public bool SummaryDisplayed() => ItinerarySummary.Displayed;

        public bool isDisplayedList()
        {
            IList<IWebElement> list = SearchResultsList.Where(x => x.Displayed).ToList();
            if (list.Count >= 0)
            { }
            return true;
        }
    }
}
