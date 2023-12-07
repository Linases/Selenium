using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using Utilities;
using Wrappers;

namespace BookingPages
{
    public class TabsKeyPage
    {
        private readonly IWebDriver _driver;
        private By SkipToMain => (By.XPath("//*[@class='bui-list-item']"));
        private By SearchFieldLocator => (By.XPath("//input[@placeholder='Where are you going?']"));
        private By AutocompleteList => (By.XPath("//*[@id='autocomplete-results']//*[@class='a3332d346a']"));
        private By MonthsYearsList => (By.XPath("//select[@data-name='year-month']/option"));
        private By ListDays => (By.XPath("//*[@data-name='day']/option"));
        private ReadOnlyCollection<IWebElement> CurrencyChoices => _driver.FindElements(By.XPath("//*[@data-testid='selection-item']"));
        private Button CurrencyButton => new Button(_driver.FindElement(By.XPath("//*[@data-testid='header-currency-picker-trigger']")));
        private WebPageElement CurrencyTitle => new WebPageElement(_driver.FindElement(By.XPath("//*[@data-testid='header-currency-picker-trigger']/span")));
        private Button SkipToMainButton => new Button(SkipToMain);
        private Button Register => new Button(_driver.FindElement(By.XPath("//*[@data-testid='header-sign-up-button']")));
        private TextBox SearchField => new TextBox(_driver.FindElement(By.XPath("//input[@placeholder='Where are you going?']")));
        private WebPageElement Menu => new WebPageElement(_driver.FindElement(By.XPath("//*[@data-testid='web-shell-header-mfe']")));
        private ReadOnlyCollection<IWebElement> AutocompleteListElement => _driver.FindElements(AutocompleteList);
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
            destination = SearchField.GetAttribute("value");
           
            var isDestination = WebDriverExtensions.GetWait(_driver).Until(ExpectedConditions.TextToBePresentInElementValue(SearchFieldLocator, destination));
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

        public void TabKeySkipToMain() => TabKeytoElement(SkipToMainButton);

        public void ClickWithJavaScript()
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("document.getElementsByClassName('bui-list-item')[0].click()");
        }

        public bool IsSkipToMainDisplayed() => SkipToMainButton.IsElementDisplayed(SkipToMain);

        public bool IsAutocompleteDisplayed()
        {
            var list = _driver.GetWaitForElementsVisible(AutocompleteList);
            return list.All(x => x.Displayed);
        }
       
        public bool IsMenuDisplayed() => Menu.IsElementDisplayedJs();


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
                var currentElementText = currentElement.Text;
                var elementText = element.Text;
                if (currentElementText.Equals(elementText))
                {
                    break;
                }
            }
        }
    }
}
