using BookingPages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities;
using WebDriverExtensions = Utilities.WebDriverExtensions;

namespace Booking_Pages
{
    public class SearchPage
    {
        private readonly IWebDriver _driver;
        private By AutocompleteResultsOptions => (By.XPath("//*[@data-testid = 'autocomplete-results-options']//li"));
        private By FirstHotelFromList => (By.XPath("(//*[@data-testid='property-card-container'])"));
        private By CloseMapButton => (By.Id("b2hotelPage"));
        private By CloseButton => (By.XPath("//*[@title='Close']//*[@class= 'bk-icon -iconset-close_bold']"));
        private By CloseButtonFinishBooking => (By.XPath("//span[@class= 'abandoned-cart-growl-item__chevron bicon-rightchevron']"));
        private By SearchResults => (By.XPath("//*[contains(@aria-label, 'Search results')]"));
        private By Checkbox5stars => (By.XPath("//*[contains(@data-component,'arp-left-column')]//div[@data-filters-item='class:class=5']"));
        private By DissmissGeniusAlert => (By.CssSelector(".c0528ecc22 button"));
        private By PriceLowest => (By.XPath("//*[@data-id='price']"));
        private By ListByPrices => (By.XPath("//*[@data-testid='price-and-discounted-price']"));
        private By RoomsChoices => (By.XPath("//select[@data-testid='select-room-trigger']"));
        private IWebElement SearchField => _driver.FindElement(By.XPath("//input[@placeholder='Where are you going?']"));
        private IWebElement SearchButton => _driver.FindElement(By.XPath("//*[@type = 'submit']"));
        private IWebElement HotelsListBlock => _driver.FindElement(By.CssSelector(".d4924c9e74"));
        private IWebElement EnteredGuestsNumber => _driver.FindElement(By.XPath("//*[@data-testid='occupancy-config']"));
        private IWebElement AdultsNrMinus => _driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[1]"));
        private IWebElement AdultsNrPlus => _driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[2]"));
        private IWebElement ChildrenNrMinus => _driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[3]"));
        private IWebElement ChildrenNrPlus => _driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[4]"));
        private IWebElement RoomsNrMinus => _driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[5]"));
        private IWebElement RoomsNrPlius => _driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[6]"));
        private IWebElement DoneButton => _driver.FindElement(By.XPath("//*[text()='Done']"));
        private IWebElement CheckBookingButton => _driver.FindElement(By.XPath("//*[@data-component='booking-overview-trigger']"));
        private IWebElement BookAndPayButton => _driver.FindElement(By.CssSelector(".bui-modal__footer button"));
        private IWebElement MoreFacilities => _driver.FindElement(By.XPath("(//*[@data-filters-group='hotelfacility']//button)[1]"));
        private IWebElement FivestarsHotelsNumber => _driver.FindElement(By.XPath("//*[contains(@data-component,'arp-left-column')]//div[@data-filters-item='class:class=5']//*[@data-testid='filters-group-label-container']/span"));
        private IWebElement SortButton => _driver.FindElement(By.XPath("//*[text()='Sort by:']"));
        private IWebElement FitnessCenter => _driver.FindElement(By.XPath("//*[@data-filters-item='hotelfacility:hotelfacility=11']"));
        private IWebElement FitnessCenterCheckboxValue => _driver.FindElement(By.XPath("(//*[@data-filters-item='hotelfacility:hotelfacility=11']//*[@data-testid='filters-group-label-container']/span)[1]"));
        private IWebElement ReserveButton => _driver.FindElement(By.XPath("//*[@class='bui-button__text js-reservation-button__text']"));
        private IWebElement FirstName => _driver.FindElement(By.Id("firstname"));
        private IWebElement LastName => _driver.FindElement(By.Id("lastname"));
        private IWebElement Email => _driver.FindElement(By.Id("email"));
        private IWebElement PhoneNr => _driver.FindElement(By.Id("phone"));
        private IWebElement NextDetailsButton => _driver.FindElement(By.XPath("//*[contains(@class, 'bui-button--primary')]"));

        private TabsKeyPage _tabsKeyPage;

        public SearchPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterDestination(string destination) => SearchField.SendKeys(destination);

        public void SelectAutocompleteOption()
        {
            var destination = GetDestination();
            var autocomplete = _driver.GetWaitForElementsVisible(AutocompleteResultsOptions);
            var firstMatchingOption = autocomplete.FirstOrDefault(option =>
            {
                try
                {
                    return option.Text.Contains(destination);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });

            firstMatchingOption?.Click();
        }

        public bool IsListofHotelsDisplayed()
        {
            var isDisplayed = HotelsListBlock.Displayed;
            return isDisplayed;
        }

        public void PressSearchButton() => SearchButton.Click();

        public void ClickGuestInput() => EnteredGuestsNumber.Click();

        public void SelectAdultsNr(string number)
        {
            var digitNumber = int.Parse(number);
            var existingNumber = 2;

            for (int i = 0; i < digitNumber - existingNumber; i++)
            {
                if (digitNumber == existingNumber)
                {
                    return;
                }
                if (existingNumber < digitNumber)
                {
                    AdultsNrPlus.Click();
                    existingNumber++;
                }
                else if (existingNumber > digitNumber)
                {
                    AdultsNrMinus.Click();
                    existingNumber--;
                }
            }
        }

        public void SelectAdultsNrWithKeys(string number)
        {
            _tabsKeyPage.ClickTab();
            var digitNumber = int.Parse(number);
            var existingNumber = 2;

            for (int i = 0; i < digitNumber - existingNumber; i++)
            {
                if (digitNumber == existingNumber)
                {
                    _tabsKeyPage.ClickTab();
                }
                if (existingNumber < digitNumber)
                {
                    _tabsKeyPage.ClickArrowRight();
                    existingNumber++;
                }
                else if (existingNumber > digitNumber)
                {
                    _tabsKeyPage.ClickArrowLeft();
                    existingNumber--;
                }
            }
            _tabsKeyPage.ClickTab();
        }

        public void SelectChildrenNr(string number)
        {
            var digitNumber = int.Parse(number);
            var existingNumber = 0;
            for (int i = 0; i < digitNumber; i++)
            {
                if (digitNumber == existingNumber)
                {
                    return;
                }
                if (digitNumber < existingNumber)
                {
                    ChildrenNrMinus.Click();
                }
                if (digitNumber > existingNumber)
                {
                    ChildrenNrPlus.Click();
                }
            }
        }

        public void SelectChildrenNrKeys(string number)
        {
            var digitNumber = int.Parse(number);
            var existingNumber = 0;
            for (int i = 0; i < digitNumber; i++)
            {
                if (digitNumber == existingNumber)
                {
                    _tabsKeyPage.ClickTab();
                }
                if (digitNumber < existingNumber)
                {
                    _tabsKeyPage.ClickArrowRight();
                }
                if (digitNumber > existingNumber)
                {
                    _tabsKeyPage.ClickArrowLeft();
                }
            }
            _tabsKeyPage.ClickTab();
        }

        public void SelectRoomsNrWithKeys(string number)
        {
            var digitNumber = int.Parse(number);
            var existingNumber = 1;
            for (int i = 1; i < digitNumber; i++)
            {
                if (digitNumber == existingNumber)
                {
                    _tabsKeyPage.ClickTab();
                }
                if (digitNumber < existingNumber)
                {
                    _tabsKeyPage.ClickArrowLeft();
                }
                if (digitNumber > existingNumber)
                {
                    _tabsKeyPage.ClickArrowRight();
                }
            }
            _tabsKeyPage.ClickTab();
            _tabsKeyPage.ClickEnter();
            _tabsKeyPage.ClickTab();
        }

        public void SelectRoomsNr(string number)
        {
            var digitNumber = int.Parse(number);
            var existingNumber = 1;
            for (int i = 1; i < digitNumber; i++)
            {
                if (digitNumber == existingNumber)
                {
                    return;
                }
                if (digitNumber < existingNumber)
                {
                    RoomsNrMinus.Click();
                }
                if (digitNumber > existingNumber)
                {
                    RoomsNrPlius.Click();
                }
            }
        }

        public void PressDone() => DoneButton.Click();

        public string GetGuestsNrValue() => EnteredGuestsNumber.Text;

        public void Select5Stars()
        {
            var stars = _driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(Checkbox5stars));
            stars.Click();
        }

        public string GetSearchResults()
        {
            _driver.GetWait().Until(ExpectedConditions.TextToBePresentInElementLocated(SearchResults, GetFiveStartsHotelsCeckboxValue()));
            return _driver.FindElement(SearchResults).Text;
        }

        public string GetSearchResultsFitnessCenter()
        {
            _driver.GetWait().Until(ExpectedConditions.TextToBePresentInElementLocated(SearchResults, GetFitnessCenterCheckboxValue()));
            return _driver.FindElement(SearchResults).Text;
        }

        public string GetFiveStartsHotelsCeckboxValue() => FivestarsHotelsNumber.Text;

        public string GetFitnessCenterCheckboxValue() => FitnessCenterCheckboxValue.Text;

        public void DissmissAlert()
        {
            var alert = _driver.WaitForElementIsClicable(DissmissGeniusAlert);
            if (alert.Displayed)
            {
                alert.Click();
            }
        }

        public void ClickSortButton() => SortButton.Click();

        public void SelectPriceFilter()
        {
            var lowestPrice = _driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(PriceLowest));
            lowestPrice.Click();
        }

        public bool IsFilteredByLowestPrice()
        {
            var list = _driver.GetWaitForElementsVisible(ListByPrices);
            var prices = list.Select(n => n.Text).ToList();

            var sortedNames = new List<string>(prices);
            sortedNames.Sort();

            var areSorted = Enumerable.SequenceEqual(prices, sortedNames);
            return areSorted;
        }

        public void ChooseFitnessCenter()
        {
            ClickMoreFacilities();
            var fitness = _driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(FitnessCenter));
            fitness.Click();
        }

        public void SelectHotel()
        {
            var hotelslist = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(FirstHotelFromList));
            var firstMatchingOption = hotelslist.FirstOrDefault(option => option.Displayed);

            var seeAvailabilityButton = firstMatchingOption.FindElement(By.XPath("//*[text()='See availability']"));
            seeAvailabilityButton.Click();
        }

        public void SelectNumberOfRooms(string number)
        {
            try
            {
                var roomDropdowns = WebDriverExtensions.GetWait(_driver).Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(RoomsChoices));

                if (roomDropdowns.Count > 0)
                {
                    var roomNumbers = roomDropdowns.Select(element => new SelectElement(element)).ToList();
                    roomNumbers[0].SelectByValue(number);
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Alert dismissal element did not appear within the specified time.");
            }
        }

        public void ClickReserve() => ReserveButton.Click();

        public void EnterFirstName(string firstName) => FirstName.SendKeys(firstName);

        public void EnterLastName(string lastName) => LastName.SendKeys(lastName);

        public void EnterEmail(string email) => Email.SendKeys(email);

        public void EnterPhoneNr(string phoneNr) => PhoneNr.SendKeys(phoneNr);

        public string GetLastName()
        {
            var js = (IJavaScriptExecutor)_driver;
            string lastNameValue = (string)js.ExecuteScript("return document.getElementById('lastname').value;");
            return lastNameValue;
        }

        public string GetFirstName()
        {
            var js = (IJavaScriptExecutor)_driver;
            string firstNameValue = (string)js.ExecuteScript("return document.getElementById('firstname').value;");
            return firstNameValue;
        }
        public string GetEmail()
        {
            var js = (IJavaScriptExecutor)_driver;
            string emailValue = (string)js.ExecuteScript("return document.getElementById('email').value;");
            return emailValue;
        }

        public string GetPhoneNr()
        {
            var js = (IJavaScriptExecutor)_driver;
            string phoneValue = (string)js.ExecuteScript("return document.getElementById('phone').value;");
            return phoneValue;
        }

        public void PressNextDetailsButton() => NextDetailsButton.Click();

        public void ClickCheckYourBooking() => CheckBookingButton.Click();

        public bool IsAvailableToClickButton()
        {
            if (BookAndPayButton.Enabled && BookAndPayButton.Displayed)
            {
                return true;
            }
            return false;
        }

        public void CloseMap()
        {
            var closeMap = _driver.GetWait().Until(ExpectedConditions.ElementToBeClickable(CloseMapButton));
            closeMap.Click();
        }

        public void ClosePopUpButton()
        {
            try
            {
                var closePopUp = WebDriverExtensions.GetWait(_driver, 20, 500).Until(ExpectedConditions.ElementToBeClickable(CloseButton));
                if (closePopUp.Displayed)
                {
                    closePopUp.Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Pop up dismissal element did not appear within the specified time.");
            }
        }

        public void CloseFinishBooking()
        {
            try
            {
                var closeFinishBooking = _driver.GetWait(5, 200).Until(ExpectedConditions.ElementToBeClickable(CloseButtonFinishBooking));
                if (closeFinishBooking.Displayed)
                {
                    closeFinishBooking.Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Close finish booking dismissal element did not appear within the specified time.");
            }
        }

        public string GetDestination() => SearchField.GetAttribute("value");

        private void ClickMoreFacilities() => MoreFacilities.Click();
    }
}
