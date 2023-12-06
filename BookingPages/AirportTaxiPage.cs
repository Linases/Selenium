using OpenQA.Selenium;
using Utilities;
using Wrappers;
using WebDriverExtensions = Utilities.WebDriverExtensions;

namespace BookingPages
{
    public class AirportTaxiPage
    {
        private readonly IWebDriver _driver;
        private Button _button = new Button();
        private TextBox _textBox = new TextBox();
        private By Auto_CompleteListPickUp => (By.CssSelector("#pickupLocation-items"));
        private By Auto_CompleteListDropOff => (By.CssSelector("#dropoffLocation-items"));
        private By SearchResultsList => (By.CssSelector(".SRM_527ba3f0"));
        private By PickUpDate => (By.XPath("//button[@data-test='rw-date-field__link--pickup']/span"));
        private By PickUpTime => (By.XPath("//button[@data-test='rw-time-field--pickup']/span"));
        private By PickUpLocation => (By.Id("pickupLocation"));
        private By DropOffLocation => (By.Id("dropoffLocation"));
        private By CurrentDays => (By.XPath("//*[@data-test='rw-calendar']//td"));
        private By ItinerarySummary => (By.XPath("//*[@data-testid='route-summary-wrapper']"));
        private TextBox PickUpLocationInput => new TextBox(_driver.FindElement(PickUpLocation));
        private TextBox Destination => new TextBox(_driver.FindElement(DropOffLocation));
        private Button PickUpDateButton => new Button(_driver.FindElement(PickUpDate));
        private TextBox PickUpDateText => new TextBox(_driver.FindElement(PickUpDate));
        private Button PickUpTimeButton => new Button(_driver.FindElement(PickUpTime));
        private TextBox PickUpTimeText => new TextBox(_driver.FindElement(PickUpTime));
        private Button SearchButton => new Button(_driver.FindElement(By.XPath("(//span[@data-test='button-content'])[1]")));
        private Button Calendar => new Button(_driver.FindElement(By.XPath("//*[@data-test='rw-calendar']")));
        private TextBox CurrentMonth => new TextBox(Calendar.FindElement(By.CssSelector(".rw-c-date-picker__calendar-caption")));
        private Button NextMonthArrow => new Button(Calendar.FindElement(By.XPath("//*[@data-test='rw-date-picker__btn--next']")));
        private DropDown SelectHour => new DropDown(_driver.FindElement(By.CssSelector("#pickupHour")));
        private DropDown SelectMinutes => new DropDown(_driver.FindElement(By.CssSelector("#pickupMinute")));
        private Button ConfirmTimeButton => new Button(_driver.FindElement(By.XPath("//*[@data-test='rw-time-picker__confirm-button']")));
        private Button ContinueButton => new Button(_driver.FindElement(By.XPath("//button[@data-test='continue-action-bar__continue-button']")));

        public AirportTaxiPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterPickUpLocation(string pickUp)
        {
            PickUpLocationInput.ClearAndEnterText(pickUp);
            WebDriverExtensions.GetWait(_driver).Until(c => c.WaitForElementsVisible(Auto_CompleteListPickUp));
            PickUpLocationInput.SendKeys(Keys.Enter);
        }

        public void EnterDestinationLocation(string whereTo)
        {
            Destination.ClearAndEnterText(whereTo);
            WebDriverExtensions.GetWait(_driver).Until(c => c.WaitForElementsVisible(Auto_CompleteListDropOff));
            Destination.SendKeys(Keys.Enter);
        }

        public string GetPickUpLocation()
        {
            var value = PickUpLocationInput.GetAttribute("Id");
            var location = _textBox.GetTextWithJsById(_driver, value);
            return location;
        }

        public string GetDropOffLocation()
        {
            var value = Destination.GetAttribute("Id");
            var destination = _textBox.GetTextWithJsById(_driver, value);
            return destination;
        }

        public void ClickDateField() => PickUpDateButton.Click();

        public void SelectDate(DateTime taxiDate)
        {
            var desiredMonthYearText = taxiDate.ToString("MMMM yyyy");
            _button.ClickWhenDoNotContainText(CurrentMonth, desiredMonthYearText, NextMonthArrow);
            _button.ClickFirstThatContainsText(_driver, CurrentDays, $"{taxiDate.Day}");
        }

        public string GetSelectedDate() => PickUpDateText.Text;

        public void ClickTimeField() => PickUpTimeButton.Click();

        public void ConfirmTime() => ConfirmTimeButton.Click();

        public void SelectHourValue(string hour) => SelectHour.SelectByValue(hour);

        public void SelectMinutesValue(string minutes) => SelectMinutes.SelectByValue(minutes);

        public string GetPickUpTime() => PickUpTimeText.Text;

        public void ClickSearch() => SearchButton.Click();

        public void SelectTaxi() => _button.ClickLastFromList(_driver, SearchResultsList);

        public void ClickContinueButton() => ContinueButton.Click();

        public bool IsSummaryDisplayed() => _button.IsElementDisplayed(_driver, ItinerarySummary);

        public bool IsDisplayedList() => _button.IsListDisplayed(_driver, SearchResultsList);
    }
}
