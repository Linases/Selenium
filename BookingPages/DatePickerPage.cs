using OpenQA.Selenium;

namespace BookingPages
{
    public class DatePickerPage
    {
        private readonly IWebDriver _driver;
        private IWebElement CheckIn_OutDatesOutput => _driver.FindElement(By.CssSelector(".f73e6603bf"));
        private IWebElement SelectedCheckInDay => _driver.FindElement(By.XPath("//*[@data-testid='date-display-field-start']"));
        private IWebElement SelectedCheckOutDay => _driver.FindElement(By.XPath("//*[@data-testid='date-display-field-end']"));
        private IWebElement Calendar => _driver.FindElement(By.CssSelector("#calendar-searchboxdatepicker"));
        private IWebElement NextMonthArrow => _driver.FindElement(By.XPath("//*[@data-testid='searchbox-datepicker-calendar']/button"));
        private IList<IWebElement> CurrentMonths => Calendar.FindElements(By.TagName("h3"));
        private IList<IWebElement> CurrentDays => Calendar.FindElements(By.TagName("td"));

        public DatePickerPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void OpenCalendar() => CheckIn_OutDatesOutput.Click();

        public void SelectDate(DateTime dateToSelect)
        {
            var currentMonthYearText = CurrentMonths.Select(x => x.Text).ToList();
            var desiredMonthYearText = dateToSelect.ToString("MMMM yyyy");
            while (!currentMonthYearText.Contains(desiredMonthYearText))
            {
                NextMonthArrow.Click();
            }
            var desiredDayElement = CurrentDays.FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}"));
            desiredDayElement.Click();
        }

        public string GetSelectedCheckInDay() => SelectedCheckInDay.Text;

        public string GetSelectedCheckOutDay() => SelectedCheckOutDay.Text;
    }
}
