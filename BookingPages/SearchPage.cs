using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using SeleniumExtras.WaitHelpers;
using Utilities;

namespace Booking_Pages
{
    public class SearchPage
    {
        private readonly IWebDriver _driver;
        private IWebElement SearchField => _driver.NoSuchElementExceptionWait(By.XPath("//input[@placeholder='Where are you going?']"), 30,1000);
        private IList<IWebElement> AutocompleteResultsOptions => _driver.WaitForElementsVisible(By.XPath("//*[@data-testid = 'autocomplete-results-options']//li"), 20);
        private IWebElement FirstRelevantOption => _driver.WaitForElementVisible(By.XPath("//*[@id='autocomplete-result-0']"), 20);
        private IWebElement SearchButton => _driver.WaitForElementVisible(By.XPath("//*[@type = 'submit']"), 20);

        private IWebElement HotelsListBlock => _driver.NoSuchElementExceptionWait(By.CssSelector(".d4924c9e74"), 20,500);

        public SearchPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterDestination(string destination) => SearchField.SendKeys(destination);

        public bool IsAutocompleteOptionValid()
        {
            return AutocompleteResultsOptions.Select(x => x.Text).ToList().Equals(GetDestination());
        }

        public void SelectAutocompletedOption ()
        {
            if (IsAutocompleteOptionValid()== true)
            {
                FirstRelevantOption.Click();
            }
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

    }
}
