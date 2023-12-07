using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Utilities;
using Wrappers;

namespace BookingPages
{
    public class DatePickerPage
    {
        private readonly IWebDriver _driver;

        private By CurrentDays => (By.CssSelector("#calendar-searchboxdatepicker td"));
        private Button CheckIn_OutDatesOutput => new Button(_driver.FindElement(By.CssSelector(".f73e6603bf")));
        private TextBox SelectedCheckInDay => new TextBox(_driver.FindElement(By.XPath("//*[@data-testid='date-display-field-start']")));
        private TextBox SelectedCheckOutDay => new TextBox(_driver.FindElement(By.XPath("//*[@data-testid='date-display-field-end']")));
        private Button NextMonthArrow => new Button(_driver.FindElement(By.XPath("//*[@data-testid='searchbox-datepicker-calendar']/button")));
        private ReadOnlyCollection<IWebElement> CurrentMonths => _driver.FindElements(By.CssSelector("#calendar-searchboxdatepicker h3"));

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
            var expectedDay = _driver.WaitForElementsVisible(CurrentDays).FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}"));
            var element = new Button(expectedDay);
            element.Click();
        }

        public string GetSelectedCheckInDay() => SelectedCheckInDay.Text;

        public string GetSelectedCheckOutDay() => SelectedCheckOutDay.Text;
    }
}

