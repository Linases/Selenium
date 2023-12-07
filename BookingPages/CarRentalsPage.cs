using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Utilities;
using Wrappers;

namespace BookingPages
{
    public class CarRentalsPage
    {
        private readonly IWebDriver _driver;
        private By CurrentDays => (By.XPath("//*[@data-testid='bui-calendar']//td"));
        private Button SearchButton => new Button(_driver.FindElement(By.CssSelector(".submit-button-container")));
        private WebPageElement ErrorMessage => new WebPageElement(_driver.FindElement(By.XPath("//*[text()='Please provide a pick-up location']")));
        private TextBox PickUpLocation => new TextBox(_driver.FindElement(By.CssSelector("#searchbox-toolbox-fts-pickup")));
        private Button PickUpDate => new Button(_driver.FindElement(By.XPath("//*[@data-testid ='searchbox-toolbox-date-picker-pickup-date-value']")));
        private WebPageElement DropOffDate => new WebPageElement(_driver.FindElement(By.XPath("//*[@data-testid ='searchbox-toolbox-date-picker-dropoff-date-value']")));
        private Button NextMonthArrow => new Button(_driver.FindElement(By.XPath("//*[@data-testid='bui-calendar']/ button")));
        private ReadOnlyCollection<IWebElement> CurrentMonths => _driver.FindElements(By.XPath("//*[@data-testid='bui-calendar']//h3"));

        public CarRentalsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickSearchButton() => SearchButton.Click();

        public string GetErrorMessage() => ErrorMessage.Text;

        public void InputLocation(string invalidLocation) => PickUpLocation.ClearAndEnterText(invalidLocation);

        public string GetLocation() => PickUpLocation.GetAttribute("value");

        public void ClickPickUpDateField() => PickUpDate.Click();

        public string GetPickUpDate() => PickUpDate.Text;

        public string GetDropOffDate() => DropOffDate.Text;

        public void SelectDate(DateTime dateToSelect)
        {
            var currentMonthYearText = CurrentMonths.Select(x => x.Text).ToList();
            var desiredMonthYearText = dateToSelect.ToString("MMMM yyyy");
            while (!currentMonthYearText.Contains(desiredMonthYearText))
            {
                NextMonthArrow.Click();
            }
            var day = _driver.WaitForElementsVisible(CurrentDays).FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}"));
            var expectedDay = new Button(day);
            expectedDay.Click();
        }
    }
}
