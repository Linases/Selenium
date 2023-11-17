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
        private IWebElement PickUpLocation => _driver.WaitForElementVisible(By.Id("pickupLocation"));
        private IWebElement Destination => _driver.WaitForElementVisible(By.Id("dropoffLocation"));
        private IWebElement PickUpDate => _driver.WaitForElementVisible(By.XPath("//button[@data-test='rw-date-field__link--pickup']/span"));
        private IWebElement PickUpTime => _driver.WaitForElementVisible(By.XPath("//button[@data-test='rw-time-field--pickup']/span"));
        private IWebElement SearchButton => _driver.WaitForElementVisible(By.XPath("//*[@id='booking-taxi-searchbar__container']/div/div/div/form/div[2]/div[1]/div[2]/div/div/div[3]/div/button"));

        private IWebElement Calendar => _driver.WaitForElementVisible(By.XPath("//*[@data-test='rw-calendar']"));
        private IWebElement CurrentMonth => Calendar.FindElement(By.CssSelector(".rw-c-date-picker__calendar-caption"));
        private IWebElement NextMonthArrow => Calendar.FindElement(By.XPath("//*[@data-test='rw-date-picker__btn--next']"));
        private IWebElement Auto_CompleteListFirstItem => _driver.WaitForElementClicable(By.XPath("//*[@data-test='rw-autocomplete-item__title-0']"));
        private IList<IWebElement> CurrentDays => Calendar.FindElements(By.XPath("//*[@data-test='rw-calendar']//td"));
        private IList<IWebElement> SearchResultsList => _driver.WaitForElementsVisible(By.CssSelector(".SRM_527ba3f0"));

        private SelectElement SelectHour => new SelectElement(_driver.WaitForElementVisible(By.CssSelector("#pickupHour")));
        private SelectElement SelectMinutes => new SelectElement(_driver.WaitForElementClicable(By.CssSelector("#pickupMinute")));
        private IWebElement ConfirmTimeButton => _driver.WaitForElementClicable(By.XPath("//*[@data-test='rw-time-picker__confirm-button']"));
        private IWebElement ContinueButton => _driver.WaitForElementClicable(By.XPath("//button[@data-test='continue-action-bar__continue-button']"));
        private IWebElement ItinerarySummary => _driver.WaitForElementVisible(By.XPath("//*[@data-testid='route-summary-wrapper']"));
        public AirportTaxiPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterPickUpLocation(string pickUpLocation) => PickUpLocation.SendKeys(pickUpLocation);
        public void EnterDestinationLocation(string destination) => Destination.SendKeys(destination);

        public void ChooseFirstItem() => Auto_CompleteListFirstItem.Click();

        public string GetPickUpLocation() => PickUpLocation.Text;
        public string GetDestination() => Destination.Text;
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
