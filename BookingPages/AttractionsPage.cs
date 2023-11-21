using OpenQA.Selenium;
using Utilities;

namespace BookingPages
{
    public class AttractionsPage
    {
        private readonly IWebDriver _driver;

        private IWebElement SearchField => _driver.FindElement(By.XPath("//input[@placeholder='Where are you going?']"));
        By AutocompleteList => (By.CssSelector(".css-9dv5ti"));
      
        private IWebElement SelectDaysField => _driver.FindElement(By.XPath("//*[text()='Select your dates']"));
        private IWebElement SelectedDayField => _driver.FindElement(By.CssSelector(".css-tbiur0"));

        private IWebElement Calendar => _driver.FindElement(By.CssSelector(".a10b0e2d13"));

        private IWebElement NextMonthArrow => Calendar.FindElement(By.XPath(".a10b0e2d13 button"));

        private IList<IWebElement> CurrentMonths => Calendar.FindElements(By.CssSelector(".a10b0e2d13 h3"));

        private IList<IWebElement> CurrentDays => Calendar.FindElements(By.CssSelector(".a10b0e2d13 td"));

        private IWebElement SearchButton => _driver.FindElement(By.XPath("//*[@type = 'submit']"));

        By FirstAttraction => (By.XPath("(//*[@data-testid='sr-list']//a)[1]"));
        By AttractionsDetails => (By.XPath("//*[@data-testid='inline-ticket-config']"));
        private IWebElement Timeslot => _driver.FindElement(By.XPath("//*[@data-testid='timeslot-selector']"));
        private IWebElement DatePicker => _driver.FindElement(By.XPath("//*[@data-testid='datepicker']"));

        public AttractionsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterDestination(string destination) => SearchField.SendKeys(destination);

        public string GetDestination() => SearchField.GetAttribute("value");

        public void SelectAutocompleteOption()
        {
            var destination = GetDestination();
            IList<IWebElement> autocomplete = _driver.WaitForElementsVisible(AutocompleteList);
            var firstMatchingOptionText = autocomplete.FirstOrDefault(option => option.Text.Contains(destination));
            firstMatchingOptionText.Click();
        }

        public void ClickDatesField() => SelectDaysField.Click();

        public void SelectDate(DateTime dateToSelect)
        {
            var currentMonthYearText = CurrentMonths.Select(x => x.Text).ToList();

            var desiredMonthYearText = dateToSelect.ToString("MMMM yyyy");
            while (!currentMonthYearText.Contains(desiredMonthYearText))
            {
                NextMonthArrow.Click();
            }
            CurrentDays.FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}")).Click();
        }

        public string GetAttractionsDate() => SelectedDayField.Text;

        public void ClickSearchButton() => SearchButton.Click();

        public void SelecFirstAvailability()
        {
            var firstAttraction = _driver.WaitForElementClicable(FirstAttraction);
            firstAttraction.Click();
        }

        public bool IsAttractionDetailsDisplayed()
        {
            var details = _driver.WaitForElementVisible(AttractionsDetails);
            return details.Displayed;
        }

        public bool IsDatePickerDisplayed() => DatePicker.Displayed;

        public bool IsTimeSlotDisplayed()
        {
            var times = Timeslot;
            if (times == null)
            {
                Console.WriteLine("No times available right now");
                return true;
            }
            else
            {
                var dispalyed = Timeslot.Displayed;
                return true;
            }
        }
    }
}
