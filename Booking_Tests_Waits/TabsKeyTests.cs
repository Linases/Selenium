using Booking_Pages;
using BookingPages;
using NUnit.Framework;
using Utilities;

namespace Booking_Tests_Waits
{
    [TestFixture]
    internal class TabsKeyTests : BaseTest
    {
        private TabsKeyPage _tabsKeyPage;
        private DatePickerPage _datePickerPage;
        private HomePage _homePage;
        private SearchPage _searchPage;
        private string _existingCurrency = "EUR";
        private string _changedCurrency = "XOF";
        private string _location = "New York";
        private string _halfLocation = "New Yo";
        private string _expectedLocation = "Central New York City";
        private DateTime _checkInDate = new DateTime(2023, 12, 02);
        private DateTime _checkOutDate = new DateTime(2023, 12, 05);
        private const string _expectedAdultNr = "3";
        private const string _expectedChildrenNr = "0";
        private const string _expectedRoomsNr = "2";

        [SetUp]
        public void Setup()
        {
            _tabsKeyPage = new TabsKeyPage(Driver);
            _homePage = new HomePage(Driver);
            _searchPage = new SearchPage(Driver);
            _datePickerPage = new DatePickerPage(Driver);
        }

        [Test]
        public void NavigateToRegisterWithKeyboard()
        {
            _homePage.DissmissAlert();
            _tabsKeyPage.TabKeyToRegister();
            WebDriverExtensions.GetWait(Driver).Until(wd => wd.Url.Contains("sign-in"));
            Assert.That(Driver.Url.Contains("sign-in"), Is.True, "Registration page page is not displayed");
        }

        [Test]
        public void ChangeCurrencyWithKeyboardNavigation()
        {
            _homePage.DissmissAlert();
            var currencyNow = _tabsKeyPage.GetCurrencyName();
            Assert.That(currencyNow.Equals($"{_existingCurrency}"), $"Currency now is not {_existingCurrency}");
            _tabsKeyPage.ChangeCurrencyWithKeyNavigation();
            var currencyChanged = _tabsKeyPage.GetCurrencyName();
            Assert.That(currencyChanged.Equals($"{_changedCurrency}"), $"Currency now is not {_changedCurrency}");
        }

        [Test]
        public void SearchForStaysInSpecificLocation()
        {
            _homePage.DissmissAlert();
            _tabsKeyPage.EnterLocation(_location);
            var destination = _searchPage.GetDestination();
            Assert.That(destination, Is.EqualTo(_location), $" {_location} is not entered destination");
            _tabsKeyPage.EnterDate(_checkInDate);
            _tabsKeyPage.EnterDate(_checkOutDate);
            _tabsKeyPage.ClickTab();
            var checkIn = _datePickerPage.GetSelectedCheckInDay();
            var _checkInDateString = _checkInDate.ToString("ddd, MMM d");
            Assert.That(checkIn, Is.EqualTo(_checkInDateString), $"Check In day is not eaqual {_checkInDate}");
            var checkOut = _datePickerPage.GetSelectedCheckOutDay();
            var _checkOutDateString = _checkOutDate.ToString("ddd, MMM d");
            Assert.That(checkOut, Is.EqualTo(_checkOutDateString), $"Check Out day is not eaqual {_checkOutDate}");
            _tabsKeyPage.ClickEnter();
            _searchPage.SelectAdultsNrWithKeys(_expectedAdultNr);
            _searchPage.SelectChildrenNrKeys(_expectedChildrenNr);
            _searchPage.SelectRoomsNrWithKeys(_expectedRoomsNr);
            var actualGuestsNr = _searchPage.GetGuestsNrValue();
            var expectedGuestsNr = $"{_expectedAdultNr} adults · {_expectedChildrenNr} children · {_expectedRoomsNr} rooms";
            Assert.That(actualGuestsNr, Is.EqualTo(expectedGuestsNr), "The selected numbers of adults, children and rooms are not filled in");
            _tabsKeyPage.ClickEnter();
            Assert.That(_searchPage.IsListofHotelsDisplayed(), Is.True, "List of hotels is not diplayed");
        }

        [Test]
        public void SelectCityFromAutoComplete()
        {
            _homePage.DissmissAlert();
            _tabsKeyPage.EnterLocation(_halfLocation);
            Assert.That(_tabsKeyPage.IsAutocompleteDisplayed(), Is.True);
            _tabsKeyPage.EnterLocationWithAutocomplete(_expectedLocation);
            _tabsKeyPage.ClickTab();
            Assert.That(_tabsKeyPage.IsDestination(_expectedLocation), Is.True, $" {_expectedLocation} is not entered destination from autocomplete");
        }

        [Test]
        public void SkipToMainContent()
        {
            _homePage.DissmissAlert();
            _tabsKeyPage.TabKeySkipToMain();
            Assert.That(_tabsKeyPage.IsSkipToMainDisplayed(), Is.True);
            _tabsKeyPage.ClickWithJavaScript();
            Assert.That(_tabsKeyPage.IsMenuDisplayed(), Is.False);
            _tabsKeyPage.ClickShiftTabKeys();
            Assert.That(_tabsKeyPage.IsMenuDisplayed(), Is.True);
        }
    }
}
