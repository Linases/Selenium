using OpenQA.Selenium;

namespace BookingPages
{
    public class CarRentalsPage
    {
        private readonly IWebDriver _driver;
        private IWebElement SearchButton => _driver.FindElement(By.CssSelector(".submit-button-container"));
        private IWebElement ErrorMessage => _driver.FindElement(By.XPath("//*[text()='Please provide a pick-up location']"));
        private IWebElement PickUpLocation => _driver.FindElement(By.CssSelector("#searchbox-toolbox-fts-pickup"));
        private IWebElement PickUpDate => _driver.FindElement(By.XPath("//*[@data-testid ='searchbox-toolbox-date-picker-pickup-date-value']"));
        private IWebElement DropOffDate => _driver.FindElement(By.XPath("//*[@data-testid ='searchbox-toolbox-date-picker-dropoff-date-value']"));
        private IWebElement Calendar => _driver.FindElement(By.XPath("//*[@data-testid='bui-calendar']"));
        private IWebElement NextMonthArrow => Calendar.FindElement(By.XPath("//*[@data-testid='bui-calendar']/ button"));
        private IList<IWebElement> CurrentMonths => Calendar.FindElements(By.XPath("//*[@data-testid='bui-calendar']//h3"));
        private IList<IWebElement> CurrentDays => Calendar.FindElements(By.XPath("//*[@data-testid='bui-calendar']//td"));

        public CarRentalsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickSearchButton() => SearchButton.Click();

        public string GetErrorMessage() => ErrorMessage.Text;

        public void InputLocation(string invalidLocation) => PickUpLocation.SendKeys(invalidLocation);

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
            CurrentDays.FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}")).Click();
        }
    }
}
