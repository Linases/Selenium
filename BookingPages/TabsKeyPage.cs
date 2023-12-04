using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using Utilities;

namespace BookingPages
{
    public class TabsKeyPage
    {
        private readonly IWebDriver _driver;
        private By SearchFieldLocator => (By.XPath("//input[@placeholder='Where are you going?']"));
        private By AutocompleteList => (By.XPath("//*[@id='autocomplete-results']//*[@class='a3332d346a']"));
        private By MonthsYearsList => (By.XPath("//select[@data-name='year-month']/option"));
        private By ListDays => (By.XPath("//*[@data-name='day']/option"));
        private IList<IWebElement> CurrencyChoices => _driver.FindElements(By.XPath("//*[@data-testid='selection-item']"));
        private IWebElement CurrencyButton => _driver.FindElement(By.XPath("//*[@data-testid='header-currency-picker-trigger']"));
        private IWebElement CurrencyTitle => _driver.FindElement(By.XPath("//*[@data-testid='header-currency-picker-trigger']/span"));
        private IWebElement SkipToMain => _driver.FindElement(By.XPath("//*[@class='bui-list-item']"));
        private IWebElement Register => _driver.FindElement(By.XPath("//*[@data-testid='header-sign-up-button']"));
        private IWebElement SearchField => _driver.FindElement(By.XPath("//input[@placeholder='Where are you going?']"));
        private IWebElement Menu => _driver.FindElement(By.XPath("//*[@data-testid='web-shell-header-mfe']"));

        public TabsKeyPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterLocation(string location)
        {
            TabKeytoElement(SearchField);
            SearchField.SendKeys(location);
        }

        public void EnterLocationWithAutocomplete(string expctedLocation)
        {
            var list = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(AutocompleteList));
            foreach (var element in list)
            {
                ClickArrowDown();
                if (element.Text.Contains(expctedLocation))
                {
                    ClickEnter();
                    break;
                }
            }
        }

        public bool IsDestinationEntered(string destination)
        {
            var isDestination = WebDriverExtensions.GetWait(_driver, 20, 500).Until(ExpectedConditions.TextToBePresentInElementValue(SearchFieldLocator, SearchField.GetAttribute("value")));
            return isDestination;
        }

        public void EnterDate(DateTime date)
        {
            SelectFromList(MonthsYearsList, GetMonthLocator(date));
            SelectFromList(ListDays, GetDayLocator(date));
        }

        public void TabKeyToRegister()
        {
            var actions = new Actions(_driver);
            TabKeytoElement(Register);
            actions.SendKeys(Keys.Enter).Build().Perform();
        }

        public void ChangeCurrencyWithKeyNavigation()
        {
            TabKeytoElement(CurrencyButton);
            ClickEnter();
            while (true)
            {
                ClickArrowDown();
                IWebElement currentElement = _driver.SwitchTo().ActiveElement();
                var lastCurrency = CurrencyChoices.LastOrDefault(x => x.Displayed);
                if (currentElement.Equals(lastCurrency))
                {
                    ClickEnter();
                    break;
                }
            }
        }

        public string GetCurrencyName() => CurrencyTitle.Text;

        public void TabKeySkipToMain() => TabKeytoElement(SkipToMain);

        public void ClickWithJavaScript()
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("document.getElementsByClassName('bui-list-item')[0].click()");
        }

        public bool IsSkipToMainDisplayed()
        {
            var isDisplayed = SkipToMain.Displayed;
            return isDisplayed;
        }

        public bool IsAutocompleteDisplayed()
        {
            var cityList = _driver.GetWait().Until(wd => wd.WaitForElementsVisible(AutocompleteList));
            var isDisplayed = cityList.All(x => x.Displayed);
            return isDisplayed;
        }

        public bool IsMenuDisplayed()
        {
            var isDisplayed = IsElementDisplayed(Menu);
            return isDisplayed;
        }

        public void ClickTab()
        {
            var actions = new Actions(_driver);
            actions.SendKeys(Keys.Tab).Build().Perform();
        }

        public void ClickEnter()
        {
            var actions = new Actions(_driver);
            actions.SendKeys(Keys.Enter).Build().Perform();
        }

        public void ClickArrowRight()
        {
            var actions = new Actions(_driver);
            actions.SendKeys(Keys.ArrowRight).Build().Perform();
        }

        public void ClickArrowLeft()
        {
            var actions = new Actions(_driver);
            actions.SendKeys(Keys.ArrowLeft).Build().Perform();
        }

        public void ClickShiftTabKeys()
        {
            var actions = new Actions(_driver);
            actions.KeyDown(Keys.Shift).Build().Perform();
            ClickTab();
            actions.KeyUp(Keys.Shift).Build().Perform();
        }

        private void ClickArrowDown()
        {
            var actions = new Actions(_driver);
            actions.SendKeys(Keys.ArrowDown).Build().Perform();
        }

        private void ClickArrowUp()
        {
            var actions = new Actions(_driver);
            actions.SendKeys(Keys.ArrowUp).Build().Perform();
        }

        private bool IsElementDisplayed(IWebElement element)
        {
            var jsExecutor = (IJavaScriptExecutor)_driver;
            var isElementVisible = (bool)jsExecutor.ExecuteScript(@"
        var rect = arguments[0].getBoundingClientRect();
        return (
            rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        );", element);
            return isElementVisible;
        }

        private void SelectFromList(By listLocator, By elementLocator)
        {
            var list = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(listLocator));
            GoToTopList(listLocator);
            foreach (var element in list)
            {
                var expectedElement = _driver.FindElement(elementLocator);
                if (element.Equals(expectedElement))
                {
                    ClickEnter();
                    ClickTab();
                    ClickEnter();
                    break;
                }
                ClickArrowDown();
            }
        }

        private void GoToTopList(By locator)
        {
            var list = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
            foreach (var element in list)
            {
                ClickArrowUp();
                var firstElement = list.FirstOrDefault();
                if (!element.Equals(firstElement))
                {
                    ClickArrowUp();
                    break;
                }
            }
        }

        private By GetMonthLocator(DateTime date) => By.XPath($"//select[@data-name='year-month']/option[@value='{date.Year}-{date.Month}']");

        private By GetDayLocator(DateTime date) => By.XPath($"//select[@data-name='day']/option[@value='{date.Day}']");

        private void TabKeytoElement(IWebElement element)
        {
            while (true)
            {
                ClickTab();
                IWebElement currentElement = _driver.SwitchTo().ActiveElement();
                if (currentElement.Equals(element))
                {
                    break;
                }
            }
        }
    }
}
