using Booking_Pages;
using BookingPages;
using NUnit.Framework;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class SearchTests : BaseTest
    {
        private SearchPage _searchPage;
        private DatePickerPage _datePickerPage;
        private const string expectedDestination = "Paris";
        DateTime checkInDate = new DateTime(2023, 11, 25);
        DateTime checkOutDate = new DateTime(2023, 11, 28);
        private const string expectedAdultNr = "1";
        private const string expectedChildrenNr = "2";
        private const string expectedRoomsNr = "1";

        [SetUp]
        public void Setup()
        {
            _searchPage = new SearchPage(Driver);
            _datePickerPage = new DatePickerPage(Driver);
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
            _searchPage.ClickGuestInput();
            _searchPage.SelectAdultsNr(expectedAdultNr);
            var actualAdultsNr = _searchPage.GetAdultsNrValue();
            Assert.That(actualAdultsNr, Is.EqualTo(expectedAdultNr));

            _searchPage.SelectChildrenNr(expectedChildrenNr);
            var actualChildrenNr = _searchPage.GetChildrenNrValue();
            Assert.That(actualChildrenNr, Is.EqualTo(expectedChildrenNr));
            _searchPage.SelectRoomsNr(expectedRoomsNr);
            var actualRoomsNr = _searchPage.GetAdultsNrValue();
            Assert.That(actualRoomsNr, Is.EqualTo(expectedRoomsNr));

            _searchPage.PressSearchButton();
            Assert.That(_searchPage.IsListofHotelsDisplayed(), Is.True, "List of hotels is not diplayed");
            _searchPage.DissmissAlert();
            _searchPage.Select5Stars();

            _searchPage.ClickSortButton();
            _searchPage.SelectPriceFilter();
            Assert.That(_searchPage.FilteredByLowestPrice(), Is.True);
            _searchPage.ChooseFitnessCenter();
        }

        private void EnterDestination()
        {
            _searchPage.EnterDestination(expectedDestination);
            var destination = _searchPage.GetDestination();
            Assert.That(destination, Is.EqualTo(expectedDestination), $" {expectedDestination} is not entered destination");
        }

        private void EnterDestinationWithAutocomplete()
        {
            _searchPage.EnterDestination(expectedDestination);
            _searchPage.SelectAutocompleteOption();
            var destination = _searchPage.GetDestination();
            Assert.That(destination.Contains($"{expectedDestination}"), Is.True, $" {expectedDestination} is not entered destination");
        }

        private void EnterCheckInCheckOutDays()
        {
            _datePickerPage.OpenCalendar();
            _datePickerPage.SelectDate(checkInDate);
            _datePickerPage.SelectDate(checkOutDate);
            var checkIn = _datePickerPage.GetSelectedCheckInDay();
            DateTime parsedDate = DateTime.ParseExact(checkIn, "ddd, MMM dd", System.Globalization.CultureInfo.InvariantCulture);
            Assert.That(parsedDate, Is.EqualTo(checkInDate), $"Check In day is not eaqual {checkInDate} ");
            var checkOut = _datePickerPage.GetSelectedCheckOutDay();
            DateTime parsedCheckout = DateTime.ParseExact(checkOut, "ddd, MMM dd", System.Globalization.CultureInfo.InvariantCulture);
            Assert.That(parsedCheckout, Is.EqualTo(checkOutDate), $"Check Out day is not eaqual {checkOutDate} ");
        }
    }
}

