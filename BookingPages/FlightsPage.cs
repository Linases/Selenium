using OpenQA.Selenium;
using Utilities;

namespace BookingPages
{
    public class FlightsPage
    {
        private readonly IWebDriver _driver;
        private IWebElement FlightMode => _driver.FindElement(By.XPath("//*[text()='Round-trip']"));
        private IWebElement Milticity => _driver.FindElement(By.CssSelector("#multicity"));
        private IList<IWebElement> MultipleSearchForms => _driver.FindElements(By.XPath("//*[contains(@class, 'multicityContainer')]/div"));
        private IList<IWebElement> DepartureInputFields => _driver.FindElements(By.XPath("//*[contains(@class, 'origin')]"));
        private IList<IWebElement> DestinatonsInputFields => _driver.FindElements(By.XPath("//*[contains(@class, 'destination')]"));
        private IList<IWebElement> DateFields => _driver.FindElements(By.CssSelector(".cQtq-value"));
        By Calendar => (By.CssSelector(".c8GSD-content"));
        By NextMonthArrow => (By.XPath("//*[@aria-label='Next month']"));
        private IList<IWebElement> CurrentMonths => _driver.FindElements(By.CssSelector(".wHSr-monthName"));
        private IList<IWebElement> CurrentDays => _driver.FindElements(By.CssSelector(".onx_-days div"));
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

        public void SendKeysToDepartures(IList<IWebElement> elements, List<string> keysToSend)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Click();
                var findInputElement = _driver.WaitForElementClicable(By.XPath("//*[@placeholder ='From?']"));
                if (!string.IsNullOrEmpty(findInputElement.GetAttribute("value")))
                {
                    findInputElement.Clear();
                }
                findInputElement.SendKeys(keysToSend[i]);
                findInputElement.SendKeys(Keys.Enter);
            }
        }

        public void SendKeysToDestinatons(IList<IWebElement> elements, List<string> keysToSend)
        {

            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Click();
                var findInputElement = _driver.WaitForElementClicable(By.XPath("//*[@placeholder ='To?']"));
                if (!string.IsNullOrEmpty(findInputElement.GetAttribute("value")))
                {
                    findInputElement.Clear();
                }
                findInputElement.SendKeys(keysToSend[i]);
                findInputElement.SendKeys(Keys.Enter);
            }
        }

        public IList<IWebElement> DepartureInputFieldsToList() => DepartureInputFields.ToList();

        public IList<IWebElement> DestinationsInputFieldsToList() => DestinatonsInputFields.ToList();

        public List<string> GetEnteredDestinations() => DestinatonsInputFields.Select(x => x.Text).ToList();

        public List<string> GetEnteredDepartures() => DepartureInputFields.Select(x => x.Text).ToList();

        public List<IWebElement> DatesFieldsToList() => DateFields.ToList();

        public void SelectDates(IList<IWebElement> elements, List<DateTime> dateToSelect)
        {

            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].Click();
                var findInputElement = _driver.WaitForElementClicable(Calendar);

                var currentMonthYearText = CurrentMonths.Select(x => x.Text).ToList();

                var desiredMonthYearText = dateToSelect[i].ToString("MMMM yyyy");
                while (!currentMonthYearText.Contains(desiredMonthYearText))
                {
                    var next = _driver.WaitForElementClicable(NextMonthArrow);
                    next.Click();
                }
                CurrentDays.FirstOrDefault(element => element.Text.Contains($"{dateToSelect[i].Day}")).Click();
            }
        }

        public List<string> GetDates() => DateFields.Select(x => x.Text).ToList();

        public void ClickAdButton() => AddButton.Click();

        public void RemoveLastLeg() => RemoveLastButton.Click();

        public void ClickSearch() => SearchButton.Click();
    }
}
