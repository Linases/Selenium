using OpenQA.Selenium;
using Utilities;
using WebDriverExtensions = Utilities.WebDriverExtensions;

namespace Booking_Pages
{
    public class SearchPage
    {
        private readonly IWebDriver _driver;
        private IWebElement SearchField => _driver.FindElement(By.XPath("//input[@placeholder='Where are you going?']"));
        By AutocompleteResultsOptions => (By.XPath("//*[@data-testid = 'autocomplete-results-options']//li"));
        private IWebElement SearchButton => _driver.FindElement(By.XPath("//*[@type = 'submit']"));

        private IWebElement GuestsInputField => _driver.FindElement(By.XPath("//*[@data-testid='occupancy-config-icon']"));
        private IWebElement HotelsListBlock => _driver.FindElement(By.CssSelector(".d4924c9e74"));

        private IWebElement AdultsNr => _driver.FindElement(By.CssSelector("#group_adults"));
        private IWebElement ChildrenNr => _driver.FindElement(By.CssSelector("#group_children"));
        private IWebElement RoomsNr => _driver.FindElement(By.CssSelector("#no_rooms"));

        By Checkbox5stars => (By.XPath("//input[@id=':r12:']"));//do not find this element - timemed out
        By DissmissGeniusAlert => (By.CssSelector(".c0528ecc22 button"));

        private IWebElement SortButton => _driver.FindElement(By.XPath("//*[text()='Sort by:']"));
        private IWebElement FitnessCenter => _driver.FindElement(By.XPath("//*[@data-filters-item='hotelfacility:hotelfacility=11']"));

        private IWebElement PriceLowest => _driver.FindElement(By.XPath("//*[@data-id='price']"));

        By ListByPrices => (By.XPath("//*[@data-testid='price-and-discounted-price']"));

        public SearchPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterDestination(string destination) => SearchField.SendKeys(destination);

        public void SelectAutocompleteOption()
        {
            var destination = GetDestination();
            Thread.Sleep(1000);//work only with it
            IList<IWebElement> autocomplete = _driver.GETWaitForElementsVisible(AutocompleteResultsOptions);
            var firstMatchingOptionText = autocomplete.FirstOrDefault(option => option.Text.Contains(destination));
            firstMatchingOptionText.Click();
        }

        public bool IsListofHotelsDisplayed()
        {
            var isDisplayed = HotelsListBlock.Displayed;
            return isDisplayed;
        }

        public string GetDestination() => SearchField.GetAttribute("value");

        public void PressSearchButton()
        {
            SearchButton.Click();
        }

        public void ClickGuestInput() => GuestsInputField.Click();
        public void SelectAdultsNr(string number)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript($"arguments[0].value = '{number}';", AdultsNr);
        }

        public void SelectChildrenNr(string number)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript($"arguments[0].value = '{number}';", ChildrenNr);
        }

        public void SelectRoomsNr(string number)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript($"arguments[0].value = '{number}';", RoomsNr);
        }

        public string GetAdultsNrValue() => AdultsNr.GetAttribute("value");

        public string GetChildrenNrValue() => ChildrenNr.GetAttribute("value");

        public string GetRoomsNrValue() => RoomsNr.GetAttribute("value");

        public void Select5Stars()
        {
            var property = _driver.GETWaitForElementClicable(Checkbox5stars);
            property.Click();
        }

        public void DissmissAlert()
        {
            var alert = _driver.WaitForElementClicable(DissmissGeniusAlert);
            if (alert.Displayed)
            {
                alert.Click();
            }
        }

        public void ClickSortButton() => SortButton.Click();

        public void SelectPriceFilter() => PriceLowest.Click();

        public bool FilteredByLowestPrice()
        {
            IList<IWebElement> list = _driver.GETWaitForElementsVisible(ListByPrices);
            List<string> pricesText = list.Select(option => option.Text).ToList();

            List<decimal> prices = pricesText.Select(price =>
            {
                string numericString = new string(price.Where(char.IsDigit).ToArray());
                if (decimal.TryParse(numericString, out decimal result))
                {
                    return result;
                }
                return decimal.MaxValue;
            }).ToList();

            bool isOrderedByLowestPrice = prices.SequenceEqual(prices.OrderBy(p => p));

            return isOrderedByLowestPrice;
        }

        public void ChooseFitnessCenter() => FitnessCenter.Click();
    }
}
