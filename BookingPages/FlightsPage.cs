using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Globalization;
using Utilities;

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
        private By LocationDropdown => By.XPath("(//*[@class='c8GSD-overlay-dropdown']//li)[1]");
        private IWebElement FlightMode => _driver.FindElement(By.XPath("//*[text()='Round-trip']"));
        private IWebElement Milticity => _driver.FindElement(By.CssSelector("#multicity"));
        private IList<IWebElement> MultipleSearchForms => _driver.FindElements(By.XPath("//*[contains(@class, 'multicityContainer')]/div"));
        private IList<IWebElement> DepartureInputFields => _driver.FindElements(By.XPath("//*[@class= 'zEiP-formField zEiP-origin']"));
        private IList<IWebElement> DestinationInputFields => _driver.FindElements(By.XPath("//*[@class= 'zEiP-formField zEiP-destination']"));
        private IList<IWebElement> DateInputFields => _driver.FindElements(By.XPath("//*[@class='zEiP-formField zEiP-date']"));
        private IList<IWebElement> DateValues => _driver.FindElements(By.XPath("//*[@class='cQtq-value']"));
        private IList<IWebElement> CurrentMonths => _driver.FindElements(By.XPath("//*[@data-month]"));
        private IList<IWebElement> DaysCalendar => _driver.FindElements(By.XPath("//*[@class='onx_-days']/div"));
        private IWebElement AddButton => _driver.FindElement(By.XPath("//*[text()='Add another flight']"));
        private IWebElement RemoveLastButton => _driver.FindElement(By.XPath("(//*[@aria-label='Remove leg number 4 from your search'])[1]"));
        private IWebElement SearchButton => _driver.FindElement(By.CssSelector(".Iqt3-button-container"));

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

        public string GetDateOne() => GetFieldLine(DateValues,0).Text;

        public string GetDateTwo() => GetFieldLine(DateValues, 1).Text;

        public string GetDateThree() => GetFieldLine(DateValues, 2).Text;

        public string GetDateFour() => GetFieldLine(DateValues, 3).Text;

        public void SelectDepartureOneDate(DateTime date) => SelectDateNew(GetFieldLine(DateInputFields,0), date);

        public void SelectDepartureTwoDate(DateTime date) => SelectDateNew(GetFieldLine(DateInputFields, 1), date);

        public void SelectDepartureThreeDate(DateTime date) => SelectDateNew(GetFieldLine(DateInputFields, 2), date);

        public void SelectDepartureFourDate(DateTime date) => SelectDateNew(GetFieldLine(DateInputFields, 3), date);

        public void ClickAdButton() => AddButton.Click();

        public void RemoveLastLeg() => RemoveLastButton.Click();

        public void ClickSearch() => SearchButton.Click();

        private IWebElement GetFieldLine(IList<IWebElement> elements, int numberOfLine) => elements[numberOfLine];

        private void SendKeysTo(IWebElement element, string text, By locator)
        {
            _driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
            var findInputElement = WebDriverExtensions.GetWait(_driver).Until(ExpectedConditions.ElementToBeClickable(locator));
            if (findInputElement.GetAttribute("value").Length > 0)
            {
                findInputElement.SendKeys(Keys.Control + "a");
                findInputElement.SendKeys(Keys.Backspace);
            }
            if (findInputElement.Enabled)
            {
                findInputElement.SendKeys(text);
                var dropdown = _driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(LocationDropdown));
                dropdown.Click();
            }
        }

        private void SelectDateNew(IWebElement element, DateTime dateToSelect)
        {
            _driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
            var findDateElement = WebDriverExtensions.GetWait(_driver).Until(ExpectedConditions.ElementToBeClickable(Calendar));
            var currentMonthYearText = CurrentMonths.Select(x => x.GetAttribute("data-month").ToString()).ToList();
            IList<DateTime> ParsedMonthYear = currentMonthYearText.Select(dateString => DateTime.ParseExact(dateString, "yyyy-MM", CultureInfo.InvariantCulture)).ToList();

            int totalDays = DaysCalendar.Count;
            foreach (var item in ParsedMonthYear)
            {
                if (dateToSelect.Month > item.Month)
                {
                    var next = _driver.WaitForElementIsClicable(NextMonthArrow);
                    next.Click();
                }
                if (dateToSelect.Month < item.Month)
                {
                    var back = _driver.WaitForElementIsClicable(PreviousMonthArrow);
                    back.Click();
                }

                int daysInMonth = DateTime.DaysInMonth(item.Year, item.Month);
                for (int startIndex = 0; startIndex < totalDays - daysInMonth + 1; startIndex++)
                {
                    var daysForCurrentMonth = DaysCalendar.Skip(startIndex).Take(daysInMonth).ToList();
                    var dayToSelect = daysForCurrentMonth.FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}"));
                    if (dayToSelect != null)
                    {
                        dayToSelect.Click();
                        return;
                    }
                }
            }
        }
    }
}
