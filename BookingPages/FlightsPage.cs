using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Globalization;
using Utilities;
using Wrappers;

namespace BookingPages
{
    public class FlightsPage
    {
        private readonly IWebDriver _driver;
        private By DepartureInputValue => By.XPath("//*[@placeholder ='From?']");
        private By DestinationInputValue => By.XPath("//*[@placeholder ='To?']");
        private By Calendar => (By.CssSelector(".c8GSD-content"));
        private By NextMonthArrow => (By.XPath("//*[@aria-label='Next Month']"));
        private By PreviousMonthArrow => (By.XPath("//*[@aria-label='Previous Month']"));
        private By LocationDropdown => By.XPath("//*[@class='c8GSD-overlay-dropdown']//li");
        private Button FlightMode => new Button(By.XPath("//*[text()='Round-trip']"));
        private Button Milticity => new Button(By.CssSelector("#multicity"));
        private ReadOnlyCollection<IWebElement> MultipleSearchForms => _driver.FindElements(By.XPath("//*[contains(@class, 'multicityContainer')]/div"));
        private ReadOnlyCollection<IWebElement> DepartureInputFields => _driver.FindElements(By.XPath("//*[@class= 'zEiP-formField zEiP-origin']"));
        private ReadOnlyCollection<IWebElement> DestinationInputFields => _driver.FindElements(By.XPath("//*[@class= 'zEiP-formField zEiP-destination']"));
        private ReadOnlyCollection<IWebElement> DateInputFields => _driver.FindElements(By.XPath("//*[@class='zEiP-formField zEiP-date']"));
        private ReadOnlyCollection<IWebElement> DateValues => _driver.FindElements(By.XPath("//*[@class='cQtq-value']"));
        private ReadOnlyCollection<IWebElement> CurrentMonths => _driver.FindElements(By.XPath("//*[@data-month]"));
        private ReadOnlyCollection<IWebElement> DaysCalendar => _driver.FindElements(By.XPath("//*[@class='onx_-days']/div"));
        private Button AddButton => new Button(By.XPath("//*[text()='Add another flight']"));
        private Button RemoveLastButton => new Button(By.XPath("(//*[@aria-label='Remove leg number 4 from your search'])[1]"));
        private Button SearchButton => new Button(By.CssSelector(".Iqt3-button-container"));

        public FlightsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void PressReturn() => FlightMode.Click();

        public void SelectMulticityMode() => Milticity.Click();

        public int CountMultipleSearchForms() => MultipleSearchForms.Count();

        public void EnterDepartureOneText(string text) => SendKeysTo(GetFieldLine(DepartureInputFields, 0), text, DepartureInputValue);

        public void EnterDepartureTwoText(string text) => SendKeysTo(GetFieldLine(DepartureInputFields, 1), text, DepartureInputValue);

        public void EnterDepartureThreeText(string text) => SendKeysTo(GetFieldLine(DepartureInputFields, 2), text, DepartureInputValue);

        public void EnterDepartureFourText(string text) => SendKeysTo(GetFieldLine(DepartureInputFields, 3), text, DepartureInputValue);

        public void EnterDestinationOneText(string text) => SendKeysTo(GetFieldLine(DestinationInputFields, 0), text, DestinationInputValue);

        public void EnterDestinationTwoText(string text) => SendKeysTo(GetFieldLine(DestinationInputFields, 1), text, DestinationInputValue);

        public void EnterDestinationThreeText(string text) => SendKeysTo(GetFieldLine(DestinationInputFields, 2), text, DestinationInputValue);

        public void EnterDestinationFourText(string text) => SendKeysTo(GetFieldLine(DestinationInputFields, 3), text, DestinationInputValue);

        public string GetDepartureOne() => GetFieldLine(DepartureInputFields, 0).Text;

        public string GetDepartureTwo() => GetFieldLine(DepartureInputFields, 1).Text;

        public string GetDepartureThree() => GetFieldLine(DepartureInputFields, 2).Text;

        public string GetDepartureFour() => GetFieldLine(DepartureInputFields, 3).Text;

        public string GetDestinationOne() => GetFieldLine(DestinationInputFields, 0).Text;

        public string GetDestinationTwo() => GetFieldLine(DestinationInputFields, 1).Text;

        public string GetDestinationThree() => GetFieldLine(DestinationInputFields, 2).Text;

        public string GetDestinationFour() => GetFieldLine(DestinationInputFields, 3).Text;

        public string GetDateOne() => GetFieldLine(DateValues, 0).Text;

        public string GetDateTwo() => GetFieldLine(DateValues, 1).Text;

        public string GetDateThree() => GetFieldLine(DateValues, 2).Text;

        public string GetDateFour() => GetFieldLine(DateValues, 3).Text;

        public void SelectDepartureOneDate(DateTime date) => SelectDateNew(GetFieldLine(DateInputFields, 0), date);

        public void SelectDepartureTwoDate(DateTime date) => SelectDateNew(GetFieldLine(DateInputFields, 1), date);

        public void SelectDepartureThreeDate(DateTime date) => SelectDateNew(GetFieldLine(DateInputFields, 2), date);

        public void SelectDepartureFourDate(DateTime date) => SelectDateNew(GetFieldLine(DateInputFields, 3), date);

        public void ClickAdButton() => AddButton.Click();

        public void RemoveLastLeg() => RemoveLastButton.Click();

        public void ClickSearch() => SearchButton.Click();

        private IWebElement GetFieldLine(ReadOnlyCollection<IWebElement> elements, int numberOfLine) => elements[numberOfLine];

        private void SendKeysTo(IWebElement element, string text, By locator)
        {
            var clickElement = _driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(element));
            var newElement = new Button(clickElement);
            newElement.Click();
            var findInputElement = WebDriverExtensions.GetWait(_driver).Until(ExpectedConditions.ElementToBeClickable(locator));
            if (findInputElement.GetAttribute("value").Length > 0)
            {
                var deleteElement = new TextBox(findInputElement);
                deleteElement.DeleteAllTextWithKey();
            }
            if (findInputElement.Enabled)
            {
                findInputElement.SendKeys(text);
                var input = _driver.GetWait().Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(LocationDropdown)).FirstOrDefault();
                var firstCorrectInput = new Button(input);
                firstCorrectInput.Click();
            }
        }

        private void SelectDateNew(IWebElement element, DateTime dateToSelect)
        {
            var clickElement= _driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(element));
            var newElement = new Button(clickElement);
            newElement.Click();
            var findDateElement = WebDriverExtensions.GetWait(_driver).Until(ExpectedConditions.ElementToBeClickable(Calendar));
            var currentMonthYearText = CurrentMonths.Select(x => x.GetAttribute("data-month").ToString()).ToList();
            var ParsedMonthYear = currentMonthYearText.Select(dateString => DateTime.ParseExact(dateString, "yyyy-MM", CultureInfo.InvariantCulture)).ToList();
            int totalDays = DaysCalendar.Count;
            foreach (var item in ParsedMonthYear)
            {
                if (dateToSelect.Month > item.Month)
                {
                    var arrow = new Button(NextMonthArrow);
                    arrow.Click();
                }
                if (dateToSelect.Month < item.Month)
                {
                    var arrow = new Button(PreviousMonthArrow);
                    arrow.Click();
                }

                int daysInMonth = DateTime.DaysInMonth(item.Year, item.Month);
                for (int startIndex = 0; startIndex < totalDays - daysInMonth + 1; startIndex++)
                {
                    var daysForCurrentMonth = DaysCalendar.Skip(startIndex).Take(daysInMonth).ToList();
                    var dayToSelect = daysForCurrentMonth.FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}"));
                    var dayToSelectNew = new Button(dayToSelect);
                    if (dayToSelectNew != null)
                    {
                        dayToSelectNew.Click();
                        return;
                    }
                }
            }
        }
    }
}

