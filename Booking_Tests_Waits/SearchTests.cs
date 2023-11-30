using Booking_Pages;
using BookingPages;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using Utilities;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class SearchTests : BaseTest
    {
        private HomePage _homePage;
        private SearchPage _searchPage;
        private DatePickerPage _datePickerPage;
        private const string _expectedDestination = "Rome";
        private DateTime _checkInDate = new DateTime(2023, 11, 29);
        private DateTime _checkOutDate = new DateTime(2023, 11, 30);
        private const string _expectedAdultNr = "3";
        private const string _expectedChildrenNr = "0";
        private const string _expectedRoomsNr = "2";
        private const string _expectedNumberOfRoomsReservate = "1";
        private const string _expectedFirstName = "John";
        private const string _expectedLastName = "Doe";
        private const string _expectedEmail = "john.doe@email.com";
        private const string _expectedPhoneNr = "555-555-5555";

        [SetUp]
        public void Setup()
        {
            _homePage = new HomePage(Driver);
            _searchPage = new SearchPage(Driver);
            _datePickerPage = new DatePickerPage(Driver);
            _homePage.DissmissAlert();
        }

        [Test]
        public void SearchHotels()
        {
            EnterDestination();
            EnterCheckInCheckOutDays();
            _searchPage.PressSearchButton();
            Assert.That(_searchPage.IsListofHotelsDisplayed(), Is.True, "List of hotels is not diplayed");
        }

        [Test]
        public void SearchHotelsWithFilters()
        {
            EnterDestinationWithAutocomplete();
            EnterCheckInCheckOutDays();
            SelecGuestsRoomsNumber();

            _searchPage.PressSearchButton();
            Assert.That(_searchPage.IsListofHotelsDisplayed(), Is.True, "List of hotels is not diplayed");
            _homePage.DissmissAlert();
            _searchPage.Select5Stars();
            var expectedFiveStarPropertiesNr = _searchPage.GetFiveStartsHotelsCeckboxValue();
            var foundFiveStarPropertiesNr = _searchPage.GetSearchResults();

            Assert.That(foundFiveStarPropertiesNr.Contains(expectedFiveStarPropertiesNr), Is.True, "The list did not updated display only 5-star hotels");
            _searchPage.ClickSortButton();
            _searchPage.SelectPriceFilter();
            Assert.That(_searchPage.IsFilteredByLowestPrice(), Is.True, "The list did not updated display hotels sorted by lowest price first");

            _searchPage.ChooseFitnessCenter();
            var expectedFitnessCenterPropertiesNr = _searchPage.GetFitnessCenterCheckboxValue();
            var foundFitnessCenterPropertiesNr = _searchPage.GetSearchResultsFitnessCenter();
            Assert.That(foundFitnessCenterPropertiesNr.Contains(expectedFitnessCenterPropertiesNr), Is.True, "The list did not updated display only hotels with a fitness centre");
        }

        [Test]
        public void SearchSelectAttemptBook()
        {
            var originalWindow = Driver.CurrentWindowHandle;
            EnterDestinationWithAutocomplete();
            EnterCheckInCheckOutDays();
            SelecGuestsRoomsNumber();

            _searchPage.PressSearchButton();
            Assert.That(_searchPage.IsListofHotelsDisplayed(), Is.True, "List of hotels is not diplayed");
            _homePage.DissmissAlert();
            _searchPage.SelectHotel();
            WebDriverExtensions.GetWait(Driver).Until(wd => wd.WindowHandles.Count == 2);
            foreach (var window in Driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    Driver.SwitchTo().Window(window);
                    break;
                }
            }
            WebDriverExtensions.GetWait(Driver).Until(wd => wd.Url.Contains($"{MainUrl}/hotel/"));
            Assert.That(Driver.Url.Contains($"{MainUrl}/hotel/"), "Hotel entry page is not displayed");
            _searchPage.CloseMap();
            _searchPage.CloseFinishBooking();
            _searchPage.ClosePopUpButton();
            _searchPage.SelectNumberOfRooms(_expectedNumberOfRoomsReservate);
            _searchPage.ClickReserve();
            Assert.That(Driver.Title.Contains($"Your Details"), "A final review and personal details entry page is not displayed");

            _searchPage.EnterFirstName(_expectedFirstName);
            _searchPage.EnterLastName(_expectedLastName);
            _searchPage.EnterEmail(_expectedEmail);
            _searchPage.EnterPhoneNr(_expectedPhoneNr);
            _searchPage.PressNextDetailsButton();
            WebDriverExtensions.GetWait(Driver).Until(ExpectedConditions.TitleContains("Final Details"));
            Assert.That(Driver.Title.Contains("Final Details"), "Payments details page is not displayed");
            _searchPage.ClickCheckYourBooking();
            Assert.That(_searchPage.IsAvailableToClickButton(), Is.True, "'Book and pay' button is not available for click");
        }

        private void EnterDestination()
        {
            _searchPage.EnterDestination(_expectedDestination);
            var destination = _searchPage.GetDestination();
            Assert.That(destination, Is.EqualTo(_expectedDestination), $" {_expectedDestination} is not entered destination");
        }

        private void EnterDestinationWithAutocomplete()
        {
            _searchPage.EnterDestination(_expectedDestination);
            _searchPage.SelectAutocompleteOption();
            var destination = _searchPage.GetDestination();
            Assert.That(destination.Contains($"{_expectedDestination}"), Is.True, $" {_expectedDestination} is not entered destination");
        }

        private void EnterCheckInCheckOutDays()
        {
            _datePickerPage.OpenCalendar();
            _datePickerPage.SelectDate(_checkInDate);
            _datePickerPage.SelectDate(_checkOutDate);
            var checkIn = _datePickerPage.GetSelectedCheckInDay();
            var parsedDate = DateTime.ParseExact(checkIn, "ddd, MMM dd", System.Globalization.CultureInfo.InvariantCulture);
            Assert.That(parsedDate, Is.EqualTo(_checkInDate), $"Check In day is not eaqual {_checkInDate}");
            var checkOut = _datePickerPage.GetSelectedCheckOutDay();
            var parsedCheckout = DateTime.ParseExact(checkOut, "ddd, MMM dd", System.Globalization.CultureInfo.InvariantCulture);
            Assert.That(parsedCheckout, Is.EqualTo(_checkOutDate), $"Check Out day is not eaqual {_checkOutDate}");
        }

        private void SelecGuestsRoomsNumber()
        {
            _searchPage.ClickGuestInput();
            _searchPage.SelectAdultsNr(_expectedAdultNr);
            _searchPage.SelectChildrenNr(_expectedChildrenNr);
            _searchPage.SelectRoomsNr(_expectedRoomsNr);
            _searchPage.PressDone();

            var actualGuestsNr = _searchPage.GetGuestsNrValue();
            var expectedGuestsNr = $"{_expectedAdultNr} adults · {_expectedChildrenNr} children · {_expectedRoomsNr} rooms";
            Assert.That(actualGuestsNr, Is.EqualTo(expectedGuestsNr), "The selected numbers of adults, children and rooms are not filled in");
        }
    }
}
