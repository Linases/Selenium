using BookingPages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities;
using Wrappers;
using WebDriverExtensions = Utilities.WebDriverExtensions;

namespace Booking_Pages
{
    public class SearchPage
    {
        private readonly IWebDriver _driver;
        private TabsKeyPage _tabsKeyPage;
        private Button _button = new Button();
        private TextBox _textBox = new TextBox();
        private By AutocompleteResultsOptions => (By.XPath("//*[@data-testid = 'autocomplete-results-options']//li"));
        private By HotelsList => (By.XPath("(//*[@data-testid='property-card-container'])"));
        private By CloseMapButton => (By.Id("b2hotelPage"));
        private By CloseButton => (By.XPath("//*[@title='Close']//*[@class= 'bk-icon -iconset-close_bold']"));
        private By CloseButtonFinishBooking => (By.XPath("//span[@class= 'abandoned-cart-growl-item__chevron bicon-rightchevron']"));
        private By SearchResults => (By.XPath("//*[contains(@aria-label, 'Search results')]"));
        private By Checkbox5stars => (By.XPath("//*[contains(@data-component,'arp-left-column')]//div[@data-filters-item='class:class=5']"));
        private By DissmissGeniusAlert => (By.CssSelector(".c0528ecc22 button"));
        private By PriceLowest => (By.XPath("//*[@data-id='price']"));
        private By ListByPrices => (By.XPath("//*[@data-testid='price-and-discounted-price']"));
        private By RoomsChoices => (By.XPath("//select[@data-testid='select-room-trigger']"));
        private TextBox SearchField => new TextBox(_driver.FindElement(By.XPath("//input[@placeholder='Where are you going?']")));
        private Button SearchButton => new Button(_driver.FindElement(By.XPath("//*[@type = 'submit']")));
        private By HotelsListBlock => (By.CssSelector(".d4924c9e74"));
        private Button EnteredGuestsNumber => new Button(_driver.FindElement(By.XPath("//*[@data-testid='occupancy-config']")));
        private Button AdultsNrMinus => new Button(_driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[1]")));
        private Button AdultsNrPlus => new Button (_driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[2]")));
        private Button ChildrenNrMinus => new Button(_driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[3]")));
        private Button ChildrenNrPlus => new Button(_driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[4]")));
        private Button RoomsNrMinus => new Button(_driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[5]")));
        private Button RoomsNrPlius => new Button(_driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[6]")));
        private Button DoneButton => new Button(_driver.FindElement(By.XPath("//*[text()='Done']")));
        private Button CheckBookingButton => new Button(_driver.FindElement(By.XPath("//*[@data-component='booking-overview-trigger']")));
        private Button BookAndPayButton => new Button(_driver.FindElement(By.CssSelector(".bui-modal__footer button")));
        private Button MoreFacilities => new Button(_driver.FindElement(By.XPath("(//*[@data-filters-group='hotelfacility']//button)[1]")));
        private Button FivestarsHotelsNumber => new Button(_driver.FindElement(By.XPath("//*[contains(@data-component,'arp-left-column')]//div[@data-filters-item='class:class=5']//*[@data-testid='filters-group-label-container']/span")));
        private Button SortButton => new Button(_driver.FindElement(By.XPath("//*[text()='Sort by:']")));
        private By FitnessCenter => (By.XPath("//*[@data-filters-item='hotelfacility:hotelfacility=11']"));
        private Button FitnessCenterCheckboxValue => new Button(_driver.FindElement(By.XPath("(//*[@data-filters-item='hotelfacility:hotelfacility=11']//*[@data-testid='filters-group-label-container']/span)[1]")));
        private Button ReserveButton => new Button(_driver.FindElement(By.XPath("//*[@class='bui-button__text js-reservation-button__text']")));
        private TextBox FirstName => new TextBox(_driver.FindElement(By.Id("firstname")));
        private TextBox LastName => new TextBox(_driver.FindElement(By.Id("lastname")));
        private TextBox Email => new TextBox(_driver.FindElement(By.Id("email")));
        private TextBox PhoneNr => new TextBox(_driver.FindElement(By.Id("phone")));
        private Button NextDetailsButton => new Button(_driver.FindElement(By.XPath("//*[contains(@class, 'bui-button--primary')]")));
        private By SeeAvailabilityButton => By.XPath("//*[text()='See availability']");

        public SearchPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterDestination(string destination) => SearchField.ClearAndEnterText(destination);

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

        public bool IsListofHotelsDisplayed() => _button.IsElementDisplayed(_driver, HotelsListBlock);
      
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

        public void Select5Stars() => _button.ClickWhenReady(_driver, Checkbox5stars);

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

        public void ClickSortButton() => SortButton.Click();

        public void SelectPriceFilter() => _button.ClickWhenReady(_driver, PriceLowest);

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
            _button.ClickWhenReady(_driver, FitnessCenter);
           
        }

        public void SelectHotel() => _button.ClickFirstFromListWithElementIncluded(_driver, HotelsList, SeeAvailabilityButton);

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

        public string GetLastName() => _textBox.GetTextWithJsById(_driver, $"{LastName.GetAttribute("Id")}");
       
        public string GetFirstName() => _textBox.GetTextWithJsById(_driver, $"{FirstName.GetAttribute("Id")}");

        public string GetEmail() => _textBox.GetTextWithJsById(_driver, $"{Email.GetAttribute("Id")}");
     
        public string GetPhoneNr() => _textBox.GetTextWithJsById(_driver, $"{PhoneNr.GetAttribute("Id")}");

        public void PressNextDetailsButton() => NextDetailsButton.Click();

        public void ClickCheckYourBooking() => CheckBookingButton.Click();

        public bool IsAvailableToClickButton() => _button.IsAvailableToClickButton(BookAndPayButton);

        public void CloseMap() => _button.ClickWhenReady(_driver, CloseMapButton);

        public void ClosePopUpButton() => _button.ClickIfDisplayedTryCatch(_driver, CloseButton);

        public void CloseFinishBooking() => _button.ClickIfDisplayedTryCatch(_driver, CloseButtonFinishBooking);

        public string GetDestination() => SearchField.GetAttribute("value");

        private void ClickMoreFacilities() => MoreFacilities.Click();
    }
}
