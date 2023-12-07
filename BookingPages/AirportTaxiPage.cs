using OpenQA.Selenium;
using Utilities;
using Wrappers;
using WebDriverExtensions = Utilities.WebDriverExtensions;

namespace BookingPages
{
    public class AirportTaxiPage
    {
        private readonly IWebDriver _driver;

        private By Auto_CompleteListPickUp => (By.CssSelector("#pickupLocation-items"));
        private By Auto_CompleteListDropOff => (By.CssSelector("#dropoffLocation-items"));
        private By SearchResultsList => (By.CssSelector(".SRM_527ba3f0"));
        private By PickUpDate => (By.XPath("//button[@data-test='rw-date-field__link--pickup']/span"));
        private By PickUpTime => (By.XPath("//button[@data-test='rw-time-field--pickup']/span"));
        private By PickUpLocation => (By.Id("pickupLocation"));
        private By DropOffLocation => (By.Id("dropoffLocation"));
        private By CurrentDays => (By.XPath("//*[@data-test='rw-calendar']//td"));
        private By ItinerarySummary => (By.XPath("//*[@data-testid='route-summary-wrapper']"));
        private TextBox PickUpLocationInput => new TextBox(PickUpLocation);
        private TextBox Destination => new TextBox(DropOffLocation);
        private Button PickUpDateButton => new Button(PickUpDate);
        private TextBox PickUpDateText => new TextBox(PickUpDate);
        private Button PickUpTimeButton => new Button(PickUpTime);
        private TextBox PickUpTimeText => new TextBox(PickUpTime);
        private Button SearchButton => new Button(_driver.FindElement(By.XPath("(//span[@data-test='button-content'])[1]")));
        private WebPageElement CurrentMonth => new WebPageElement(By.CssSelector(".rw-c-date-picker__calendar-caption"));
        private Button NextMonthArrow => new Button(By.XPath("//*[@data-test='rw-date-picker__btn--next']"));
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
            var location = GetTextWithJsById(value);
            return location;
        }

        public string GetDropOffLocation()
        {
            var value = Destination.GetAttribute("Id");
            var destination = GetTextWithJsById(value);
            return destination;
        }

        public void ClickDateField() => PickUpDateButton.Click();

        public void SelectDate(DateTime taxiDate)
        {
            var desiredMonthYearText = taxiDate.ToString("MMMM yyyy");

            var currentMonthYearText = CurrentMonth.Text;

            while (!currentMonthYearText.Contains(desiredMonthYearText))
            {
                NextMonthArrow.Click();
            }
            var list = _driver.WaitForElementsVisible(CurrentDays);
            var desiredDayElement = list.FirstOrDefault(element => element.Text.Contains($"{taxiDate.Day}"));
            var dayElement = new Button(desiredDayElement);
            dayElement.Click();
        }

        public string GetSelectedDate() => PickUpDateText.Text;

        public void ClickTimeField() => PickUpTimeButton.Click();

        public void ConfirmTime() => ConfirmTimeButton.Click();

        public void SelectHourValue(string hour) => SelectHour.SelectByValue(hour);

        public void SelectMinutesValue(string minutes) => SelectMinutes.SelectByValue(minutes);

        public string GetPickUpTime() => PickUpTimeText.Text;

        public void ClickSearch() => SearchButton.Click();

        public void SelectTaxi()
        {
            var expensivePrice = _driver.GetWaitForElementsVisible(SearchResultsList).LastOrDefault(x => x.Displayed);
            var taxiElement = new Button(expensivePrice);
            taxiElement.Click();
        }

        public void ClickContinueButton() => ContinueButton.Click();

        public bool IsSummaryDisplayed()
        {
            var summary = new WebPageElement(ItinerarySummary);
            var isDisplayed = summary.IsElementDisplayed(ItinerarySummary);
            return isDisplayed;
        }

        public bool IsAnyTaxiDisplayed()
        {
            var taxiList = _driver.GetWaitForElementsVisible(SearchResultsList).Where(x => x.Displayed).ToList();
            if (taxiList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetTextWithJsById(string attributeName)
        {
            var js = (IJavaScriptExecutor)_driver;
            var stringValue = (string)js.ExecuteScript($"return document.getElementById('{attributeName}').value;");
            return stringValue;
        }
    }
}
