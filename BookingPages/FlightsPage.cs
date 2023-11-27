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
        private IWebElement DepartureInputFieldOne => _driver.FindElement(By.XPath("(//*[@class='lNCO-inner'])[1]"));
        private IWebElement DepartureInputFieldTwo => _driver.FindElement(By.XPath("(//*[@class='lNCO-inner'])[3]"));
        private IWebElement DepartureInputFieldThree => _driver.FindElement(By.XPath("(//*[@class='lNCO-inner'])[5]"));
        private IWebElement DepartureInputFieldFour => _driver.FindElement(By.XPath("(//*[@class='lNCO-inner'])[7]"));
        private IWebElement DestinationInputFieldOne => _driver.FindElement(By.XPath("(//*[@class='lNCO-inner'])[2]"));
        private IWebElement DestinationInputFieldTwo => _driver.FindElement(By.XPath("(//*[@class='lNCO-inner'])[4]"));
        private IWebElement DestinationInputFieldThree => _driver.FindElement(By.XPath("(//*[@class='lNCO-inner'])[6]"));
        private IWebElement DestinationInputFieldFour => _driver.FindElement(By.XPath("(//*[@class='lNCO-inner'])[8]"));
        private IWebElement DateInputFieldOne => _driver.FindElement(By.XPath("(//*[@class='zEiP-formField zEiP-date'])[1]"));
        private IWebElement DateInputFieldTwo => _driver.FindElement(By.XPath("(//*[@class='zEiP-formField zEiP-date'])[2]"));
        private IWebElement DateInputFieldThree => _driver.FindElement(By.XPath("(//*[@class='zEiP-formField zEiP-date'])[3]"));
        private IWebElement DateInputFieldFour => _driver.FindElement(By.XPath("(//*[@class='zEiP-formField zEiP-date'])[4]"));
        private IWebElement DateOneValue => _driver.FindElement(By.XPath("(//*[@class='cQtq-value'])[1]"));
        private IWebElement DateTwoValue => _driver.FindElement(By.XPath("(//*[@class='cQtq-value'])[2]"));
        private IWebElement DateThreeValue => _driver.FindElement(By.XPath("(//*[@class='cQtq-value'])[3]"));
        private IWebElement DateFourValue => _driver.FindElement(By.XPath("(//*[@class='cQtq-value'])[4]"));
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

        public void EnterDepartureOneText(string text) => SendKeysTo(DepartureInputFieldOne, text, DepartureInputValue);

        public void EnterDepartureTwoText(string text) => SendKeysTo(DepartureInputFieldTwo, text, DepartureInputValue);

        public void EnterDepartureThreeText(string text) => SendKeysTo(DepartureInputFieldThree, text, DepartureInputValue);
       
        public void EnterDepartureFourText(string text) => SendKeysTo(DepartureInputFieldFour, text, DepartureInputValue);
        
        public void EnterDestinationOneText(string text) => SendKeysTo(DestinationInputFieldOne, text, DestinationInputValue);
        
        public void EnterDestinationTwoText(string text) => SendKeysTo(DestinationInputFieldTwo, text, DestinationInputValue);
        
        public void EnterDestinationThreeText(string text) => SendKeysTo(DestinationInputFieldThree, text, DestinationInputValue);
        
        public void EnterDestinationFourText(string text) => SendKeysTo(DestinationInputFieldFour, text, DestinationInputValue);

        public string GetDepartureOne() => DepartureInputFieldOne.Text;
        
        public string GetDepartureTwo() => DepartureInputFieldTwo.Text;
        
        public string GetDepartureThree() => DepartureInputFieldThree.Text;
        
        public string GetDepartureFour() => DepartureInputFieldFour.Text;
        
        public string GetDestinationOne() => DestinationInputFieldOne.Text;
        
        public string GetDestinationTwo() => DestinationInputFieldTwo.Text;
        
        public string GetDestinationThree() => DestinationInputFieldThree.Text;
        
        public string GetDestinationFour() => DestinationInputFieldFour.Text;
        
        public string GetDateOne() => DateOneValue.Text;
        
        public string GetDateTwo() => DateTwoValue.Text;
        
        public string GetDateThree() => DateThreeValue.Text;
        
        public string GetDateFour() => DateFourValue.Text;

        public void SelectDepartureOneDate(DateTime date) => SelectDateNew(DateInputFieldOne, date);
        
        public void SelectDepartureTwoDate(DateTime date) => SelectDateNew(DateInputFieldTwo, date);
        
        public void SelectDepartureThreeDate(DateTime date) => SelectDateNew(DateInputFieldThree, date);
        
        public void SelectDepartureFourDate(DateTime date) => SelectDateNew(DateInputFieldFour, date);

        public void ClickAdButton() => AddButton.Click();

        public void RemoveLastLeg() => RemoveLastButton.Click();

        public void ClickSearch() => SearchButton.Click();

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
                    var next = _driver.WaitForElementClicable(NextMonthArrow);
                    next.Click();
                }
                if (dateToSelect.Month < item.Month)
                {
                    var back = _driver.WaitForElementClicable(PreviousMonthArrow);
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
