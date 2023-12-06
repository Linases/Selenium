using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Wrappers;

namespace BookingPages
{
    public class AttractionsPage
    {
        private readonly IWebDriver _driver;
        private Button _button = new Button();
        private By AutocompleteList => (By.CssSelector(".css-9dv5ti"));
        private By SelectedDayField => (By.CssSelector(".css-tbiur0"));
        private By AttractionList => (By.XPath("//*[@data-testid='sr-list']//a"));
        private By AttractionsDetails => (By.XPath("//*[@data-testid='inline-ticket-config']"));
        private By Timeslot => (By.XPath("//*[@data-testid='timeslot-selector']"));
        private By DatePicker => (By.XPath("//*[@data-testid='datepicker']"));
        private By SearchField => (By.XPath("//input[@placeholder='Where are you going?']"));
        private By CurrentDays => (By.CssSelector(".a10b0e2d13 td"));
        private TextBox SearchFieldText => new TextBox(_driver.FindElement(SearchField));
        private Button DaysFieldButton => new Button(_driver.FindElement(By.XPath("//*[text()='Select your dates']")));
        private Button NextMonthArrow => new Button(_driver.FindElement(By.CssSelector(".a10b0e2d13 button")));
        private ReadOnlyCollection<IWebElement> CurrentMonths => _driver.FindElements(By.CssSelector(".a10b0e2d13 h3"));
        private Button SearchButton => new Button(_driver.FindElement(By.XPath("//*[@type = 'submit']")));

        public AttractionsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterDestination(string destination) => SearchFieldText.ClearAndEnterText(destination);

        public string GetDestination() => SearchFieldText.GetAttribute("value");

        public void SelectAutocompleteOption()
        {
            var destination = GetDestination();
            _button.ClickFirstThatContainsText(_driver, AutocompleteList, destination);
        }

        public void ClickDatesField() => DaysFieldButton.Click();

        public void SelectDate(DateTime dateToSelect)
        {
            var desiredMonthYearText = dateToSelect.ToString("MMMM yyyy");
            _button.ClickWhenDoNotContainTextInList(CurrentMonths, desiredMonthYearText, NextMonthArrow);
            _button.ClickFirstThatContainsText(_driver, CurrentDays, $"{dateToSelect.Day}");
        }

        public string GetAttractionsDate() => _button.WaitToGetText(_driver, SelectedDayField);

        public void ClickSearchButton() => SearchButton.Click();

        public void SelecFirstAvailability() => _button.ClickFirstFromList(_driver, AttractionList);

        public bool IsAttractionDetailsDisplayed() => _button.IsElementDisplayed(_driver, AttractionsDetails);
      
        public bool IsDatePickerDisplayed() =>_button.IsElementDisplayedTryCatch(_driver, DatePicker);
       
        public bool IsTimeSlotDisplayed() =>_button.IsElementDisplayedTryCatch(_driver, Timeslot);
        }
    }
