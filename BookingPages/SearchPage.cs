﻿using BookingPages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using Utilities;
using Wrappers;
using WebDriverExtensions = Utilities.WebDriverExtensions;

namespace Booking_Pages
{
    public class SearchPage
    {
        private readonly IWebDriver _driver;
        private TabsKeyPage _tabsKeyPage;

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
        private Button AdultsNrPlus => new Button(_driver.FindElement(By.XPath("(//button[contains(@class, 'c21c56c305 ') and @type='button' and @aria-hidden='true'])[2]")));
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

        public bool IsListofHotelsDisplayed()
        {
            var hotel = new WebPageElement(HotelsListBlock);
            var isDisplayed = hotel.IsElementDisplayed(HotelsListBlock);
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
            var button = new Button(Checkbox5stars);
            button.Click();
        }

        public string GetSearchResults()=> GetTextToBePresentInElement(SearchResults, GetFiveStartsHotelsCeckboxValue());

        public string GetSearchResultsFitnessCenter() => GetTextToBePresentInElement(SearchResults, GetFitnessCenterCheckboxValue());
        
        public string GetFiveStartsHotelsCeckboxValue() => FivestarsHotelsNumber.Text;

        public string GetFitnessCenterCheckboxValue() => FitnessCenterCheckboxValue.Text;

        public void ClickSortButton() => SortButton.Click();

        public void SelectPriceFilter()
        {
            var filter = new Button(PriceLowest);
            filter.Click();
        }

        public bool IsFilteredByLowestPrice()
        {
            var list = _driver.GetWaitForElementsVisible(ListByPrices);
            var names = list.Select(n => n.Text).ToList();
            var numericValues = names.Select(p => decimal.TryParse(p, out decimal parsed) ? parsed : decimal.MinValue).ToList();
            var areSorted = numericValues.SequenceEqual(numericValues.OrderBy(p => p));
            return areSorted;
        }
       

        public void ChooseFitnessCenter()
        {
            ClickMoreFacilities();
            var fitness = new Button(FitnessCenter);
            fitness.Click();
        }

        public void SelectHotel()
        {
            var hotelslist = _driver.GetWait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(HotelsList));
            var firstMatchingOption = hotelslist.FirstOrDefault(option => option.Displayed);

            var seeAvailabilityButton = firstMatchingOption.FindElement(SeeAvailabilityButton);
            seeAvailabilityButton.Click();
        } 

        public void SelectNumberOfRooms(string number)
        {
            var selectElement = new DropDown(RoomsChoices);
            selectElement.SelectFromListByValue(RoomsChoices,number);
        }


        public void ClickReserve() => ReserveButton.Click();

        public void EnterFirstName(string firstName) => FirstName.SendKeys(firstName);

        public void EnterLastName(string lastName) => LastName.SendKeys(lastName);

        public void EnterEmail(string email) => Email.SendKeys(email);

        public void EnterPhoneNr(string phoneNr) => PhoneNr.SendKeys(phoneNr);

        public string GetLastName() => GetTextWithJsById($"{LastName.GetAttribute("Id")}");

        public string GetFirstName() => GetTextWithJsById($"{FirstName.GetAttribute("Id")}");

        public string GetEmail() =>GetTextWithJsById($"{Email.GetAttribute("Id")}");

        public string GetPhoneNr() => GetTextWithJsById($"{PhoneNr.GetAttribute("Id")}");

        public void PressNextDetailsButton() => NextDetailsButton.Click();

        public void ClickCheckYourBooking() => CheckBookingButton.Click();

        public bool IsAvailableToClickButton() => BookAndPayButton.IsAvailableToClickButton();

        public void CloseMap()
        {
            var button = new Button(CloseMapButton);
            button.ClickIfDisplayed(CloseMapButton);
        }

        public void ClosePopUpButton()
        {
            var button = new Button(CloseButton);
            button.ClickIfDisplayed(CloseButton);
        }

        public void CloseFinishBooking()
        {
            var button = new Button(CloseButtonFinishBooking);
            button.ClickIfDisplayed(CloseButtonFinishBooking);
        }

        public string GetDestination() => SearchField.GetAttribute("value");

        private void ClickMoreFacilities() => MoreFacilities.Click();

        private string GetTextWithJsById(string attributeName)
        {
            var js = (IJavaScriptExecutor)_driver;
            var stringValue = (string)js.ExecuteScript($"return document.getElementById('{attributeName}').value;");
            return stringValue;
        }
        private string GetTextToBePresentInElement(By locator, string text)
        {
            _driver.GetWait().Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
            return _driver.FindElement(locator).Text;
        }
    }
}
