using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Utilities;
using Wrappers;


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
        private By DatePicker => (By.XPath("//*[@data-testid='datepicker']"));
        private By CurrentDaysLocator => (By.CssSelector(".a10b0e2d13 td"));
        private TextBox SearchField => new TextBox(By.XPath("//input[@placeholder='Where are you going?']"));
        private Button DaysFieldButton => new Button(By.XPath("//*[text()='Select your dates']"));
        private Button NextMonthArrow => new Button(By.CssSelector(".a10b0e2d13 button"));
        private ReadOnlyCollection<IWebElement> CurrentMonths => _driver.FindElements(By.CssSelector(".a10b0e2d13 h3"));
        private Button SearchButton => new Button(By.XPath("//*[@type = 'submit']"));
        private Button AttractionsDetailsElement => new Button(AttractionsDetails);
        private Button DatePickerElement => new Button(DatePicker);
        private Button TimeslotElement => new Button(Timeslot);
        private Button SelectedDayElement => new Button(SelectedDayField);

        public AttractionsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterDestination(string destination) => SearchField.ClearAndEnterText(destination);

        public string GetDestination() => SearchField.GetAttribute("value");

        public void SelectAutocompleteOption()
        {
            var destination = GetDestination();
            var firstElement = _driver.WaitForElementsVisible(AutocompleteList).FirstOrDefault(element => element.Text.Contains($"{destination}"));
            var element = new Button(firstElement);
            element.Click();
        }

        public void ClickDatesField() => DaysFieldButton.Click();

        public void SelectDate(DateTime dateToSelect)
        {
            var desiredMonthYearText = dateToSelect.ToString("MMMM yyyy");
            var currentMonthYearText = CurrentMonths.Select(x => x.Text).ToList();
            while (!currentMonthYearText.Contains(desiredMonthYearText))
            {
                NextMonthArrow.Click();
            }

            var day = _driver.WaitForElementsVisible(CurrentDaysLocator).FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}"));
            var relevatDay = new Button(day);
            relevatDay.Click();
        }

        public string GetAttractionsDate() => SelectedDayElement.WaitToGetText(SelectedDayField);

        public void ClickSearchButton() => SearchButton.Click();

        public void SelecFirstAvailability()
        {
            var availability = _driver.WaitForElementsVisible(AttractionList).FirstOrDefault(x => x.Displayed);
            var firstAttraction = new Button(availability);
            firstAttraction.Click();
        }

        public bool IsAttractionDetailsDisplayed() => AttractionsDetailsElement.IsElementDisplayed(AttractionsDetails);

        public bool IsDatePickerDisplayed() => DatePickerElement.IsElementDisplayed(DatePicker);

        public bool IsTimeSlotDisplayed() => TimeslotElement.IsElementDisplayed(Timeslot);
    }
}
