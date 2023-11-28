using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Utilities;

namespace BookingPages
{
    public class AttractionsPage
    {
        private readonly IWebDriver _driver;
        private By AutocompleteList => (By.CssSelector(".css-9dv5ti"));
        private By SelectedDayField => (By.CssSelector(".css-tbiur0"));
        private By AttractionList => (By.XPath("//*[@data-testid='sr-list']//a"));
        private By AttractionsDetails => (By.XPath("//*[@data-testid='inline-ticket-config']"));
        private By Timeslot => (By.XPath("//*[@data-testid='timeslot-selector']"));
        private IWebElement SearchField => _driver.FindElement(By.XPath("//input[@placeholder='Where are you going?']"));
        private IWebElement SelectDaysField => _driver.FindElement(By.XPath("//*[text()='Select your dates']"));
        private IWebElement Calendar => _driver.FindElement(By.CssSelector(".a10b0e2d13"));
        private IWebElement NextMonthArrow => Calendar.FindElement(By.XPath(".a10b0e2d13 button"));
        private IList<IWebElement> CurrentMonths => Calendar.FindElements(By.CssSelector(".a10b0e2d13 h3"));
        private IList<IWebElement> CurrentDays => _driver.FindElements(By.CssSelector(".a10b0e2d13 td"));
        private IWebElement SearchButton => _driver.FindElement(By.XPath("//*[@type = 'submit']"));
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
            var autocomplete = _driver.WaitForElementsVisible(AutocompleteList);
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

        public string GetAttractionsDate() => _driver.GetWait().Until(ExpectedConditions.ElementIsVisible(SelectedDayField)).Text;

        public void ClickSearchButton() => SearchButton.Click();

        public void SelecFirstAvailability()
        {
            var attractionsList = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(AttractionList));
            var firstAttraction = attractionsList.FirstOrDefault(x => x.Displayed);
            firstAttraction.Click();
        }

        public bool IsAttractionDetailsDisplayed()
        {
            var isDisplayed = _driver.WaitForElementIsVisible(AttractionsDetails).Displayed;
            return isDisplayed;
        }

        public bool IsDatePickerDisplayed()
        {
            try
            {
                if (DatePicker == null)
                {
                    Console.WriteLine("No times available right now");
                    return true;
                }
                else
                {
                    var isDisplayed = DatePicker.Displayed;
                    return isDisplayed;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element not found. Time slot not displayed.");
                return false;
            }
        }

        public bool IsTimeSlotDisplayed()
        {
            try
            {
                if (Timeslot == null)
                {
                    Console.WriteLine("No times available right now");
                    return true;
                }
                else
                {
                    var isDisplayed = _driver.GetWait().Until(ExpectedConditions.ElementIsVisible(Timeslot)).Displayed;
                    return isDisplayed;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element not found. Time slot not displayed.");
                return false;
            }
        }
    }
}
