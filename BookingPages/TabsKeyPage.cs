using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using Utilities;

namespace BookingPages
{
    public class TabsKeyPage
    {
        private IWebDriver _driver;
        By SearchFieldLocator => (By.XPath("//input[@placeholder='Where are you going?']"));
        By AutocompleteList => (By.XPath("//*[@id='autocomplete-results']//*[@class='a3332d346a']"));
        By MonthsYearsList => (By.XPath("//select[@data-name='year-month']/option"));
        By ListDays => (By.XPath("//*[@data-name='day']/option"));
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
            var actions = new Actions(_driver);
            TabKeytoElement(SearchField);
            SearchField.SendKeys(location);
        }

        public void EnterLocationWithAutocomplete(string expctedLocation)
        {
            var list = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(AutocompleteList));
            var actions = new Actions(_driver);
            foreach (var element in list)
            {
                actions.SendKeys(Keys.ArrowDown).Build().Perform();
                if (element.Text.Contains(expctedLocation))
                {
                    actions.SendKeys(Keys.Enter).Build().Perform();
                    break;
                }
            }
        }
       
        public bool IsDestination(string destination)
        {
            var isDestination = WebDriverExtensions.GetWait(_driver, 20, 500).Until(ExpectedConditions.TextToBePresentInElementValue(SearchFieldLocator, SearchField.GetAttribute("value")));
            return isDestination;
        }

        public void EnterDate(DateTime date)
        {
            var actions = new Actions(_driver);
            actions.SendKeys(Keys.Tab).Build().Perform();
            actions.SendKeys(Keys.Enter).Build().Perform();
            var listMonths = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(MonthsYearsList));
            GoToTopListMonths();
            foreach (var element in listMonths)
            {
                var monthYear = _driver.FindElement(GetMonthLocator(date));
                if (element.Equals(monthYear))
                {
                    actions.SendKeys(Keys.Enter).Build().Perform();
                    actions.SendKeys(Keys.Tab).Build().Perform();
                    actions.SendKeys(Keys.Enter).Build().Perform();
                    break;
                }
                actions.SendKeys(Keys.ArrowDown).Build().Perform();
            }
            var listDays = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(ListDays));
            GoToTopListDays();
            foreach (var element in listDays)
            {
                var day = _driver.FindElement(GetDayLocator(date));
                if (element.Equals(day))
                {
                    actions.SendKeys(Keys.Enter).Build().Perform();
                    break;
                }
                actions.SendKeys(Keys.ArrowDown).Build().Perform();
            }
        }

        public void TabKeyToRegister()
        {
            var actions = new Actions(_driver);
            TabKeytoElement(Register);
            actions.SendKeys(Keys.Enter).Build().Perform();
        }

        public void ChangeCurrencyWithKeyNavigation()
        {
            var actions = new Actions(_driver);
            TabKeytoElement(CurrencyButton);
            actions.SendKeys(Keys.Enter).Build().Perform();
            while (true)
            {
                actions.SendKeys(Keys.ArrowDown).Build().Perform();
                IWebElement currentElement = _driver.SwitchTo().ActiveElement();
                var lastCurrency = CurrencyChoices.LastOrDefault(x => x.Displayed);
                if (currentElement.Equals(lastCurrency))
                {
                    actions.SendKeys(Keys.Enter).Build().Perform();
                    break;
                }
            }
        }

        public string GetCurrencyName() => CurrencyTitle.Text;

        public void TabKeySkipToMain()
        {
            TabKeytoElement(SkipToMain);
        }

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

        public bool IsMenuDisplayed() => IsElementDisplayed(Menu);

        private bool IsElementDisplayed(IWebElement element)
        {
            var jsExecutor = (IJavaScriptExecutor)_driver;
            bool isElementVisible = (bool)jsExecutor.ExecuteScript(@"
        var rect = arguments[0].getBoundingClientRect();
        return (
            rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
            rect.right <= (window.innerWidth || document.documentElement.clientWidth)
        );", element);
            return isElementVisible;
        }

        public void ClickTab()
        {
            var actions = new Actions(_driver);
            actions.KeyDown(Keys.Tab).Build().Perform();
        }

        public void ClickEnter()
        {
            var actions = new Actions(_driver);
            actions.KeyDown(Keys.Enter).Build().Perform();
        }

        public void ClickShiftTabKeys()
        {
            var actions = new Actions(_driver);
            actions.KeyDown(Keys.Shift).Build().Perform();
            actions.SendKeys(Keys.Tab).Build().Perform();
            actions.KeyUp(Keys.Shift).Build().Perform();
        }

        private By GetMonthLocator(DateTime date)
        {
            return (By.XPath($"//select[@data-name='year-month']/option[@value='{date.Year}-{date.Month}']"));
        }
        private By GetDayLocator(DateTime date)
        {
            return (By.XPath($"//select[@data-name='day']/option[@value='{date.Day}']"));
        }

        private void GoToTopListMonths()
        {
            var actions = new Actions(_driver);
            var listMonths = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(MonthsYearsList));
            var firstElement = listMonths.FirstOrDefault();
            {
                foreach (var item in listMonths)
                {
                    actions.SendKeys(Keys.ArrowUp).Build().Perform();
                    if (!item.Equals(firstElement))
                    {
                        break;
                    }

                }
            }
        }

        private void GoToTopListDays()
        {
            var actions = new Actions(_driver);
            var listDays = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(ListDays));
            foreach (var element in listDays)
            {
                actions.SendKeys(Keys.ArrowUp).Build().Perform();
                var firstElement = listDays.FirstOrDefault();
                if (!element.Equals(firstElement))
                {
                    actions.SendKeys(Keys.ArrowUp).Build().Perform();
                    break;
                }
            }
        }

        private void TabKeytoElement(IWebElement element)
        {
            var actions = new Actions(_driver);
            while (true)
            {
                actions.SendKeys(Keys.Tab).Build().Perform();
                IWebElement currentElement = _driver.SwitchTo().ActiveElement();
                if (currentElement.Equals(element))
                {
                    break;
                }
            }
        }
    }
}
