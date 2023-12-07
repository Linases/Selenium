﻿using OpenQA.Selenium;
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
        private By SearchField => (By.XPath("//input[@placeholder='Where are you going?']"));
        private By CurrentDaysLocator => (By.CssSelector(".a10b0e2d13 td"));
        private TextBox SearchFieldText => new TextBox(_driver.FindElement(SearchField));
        private Button DaysFieldButton => new Button(_driver.FindElement(By.XPath("//*[text()='Select your dates']")));
        private Button NextMonthArrow => new Button(_driver.FindElement(By.CssSelector(".a10b0e2d13 button")));
        private ReadOnlyCollection<IWebElement> CurrentMonths => _driver.FindElements(By.CssSelector(".a10b0e2d13 h3"));
        private Button SearchButton => new Button(_driver.FindElement(By.XPath("//*[@type = 'submit']")));
        private WebPageElement AttractionsDetailsElement => new WebPageElement(AttractionsDetails);
        private WebPageElement DatePickerElement => new WebPageElement(DatePicker);
        private WebPageElement TimeslotElement => new WebPageElement(Timeslot);
        private WebPageElement SelectedDayElement => new WebPageElement(SelectedDayField);

        public AttractionsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterDestination(string destination) => SearchFieldText.ClearAndEnterText(destination);

        public string GetDestination() => SearchFieldText.GetAttribute("value");

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
