using Booking_Pages;
using BookingPages;
using NUnit.Framework;
using System.Linq;
using System.Runtime.CompilerServices;
using Utilities;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class SearchTests : BaseTest
    {
        private SearchPage _searchPage;
        private DatePickerPage _datePickerPage;
        private const string expectedDestination = "Vilnius";
        DateTime checkInDate = new DateTime(2023, 11, 18);
        DateTime checkOutDate = new DateTime(2023, 11, 20);

        [SetUp]
        public void Setup()
        {
            _searchPage = new SearchPage(Driver);
            _datePickerPage = new DatePickerPage(Driver);
        }

        [Test]
        public void SearchHotels()
        {
            Thread.Sleep(2000);
            _searchPage.EnterDestination(expectedDestination);
            var destination = _searchPage.GetDestination();
            Assert.That(destination, Is.EqualTo(expectedDestination), $" {expectedDestination} is not entered destination");

            _datePickerPage.OpenCalendar();
            _datePickerPage.SelectDate(checkInDate);
            _datePickerPage.SelectDate(checkOutDate);
            //  var checkIn = _datePickerPage.GetSelectedCheckInDay();
            // var checkOut = _datePickerPage.GetSelectedCheckOutDay();
            //  Assert.That(_datePickerPage.GetSelectedCheckInDay().Contains($"{checkInDate}"));
            // Assert.That(_datePickerPage.GetSelectedCheckOutDay().Contains($"{checkOutDate}"));
            //Assert.That(checkOut.Equals(checkOutDate), Is.True);


            _searchPage.PressSearchButton();
            Assert.That(_searchPage.IsListofHotelsDisplayed(), Is.True, "List of hotels is not diplayed");
        }
    }
}