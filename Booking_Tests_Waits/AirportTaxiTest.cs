using Booking_Pages;
using BookingPages;
using NUnit.Framework;
using System.Globalization;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class AirportTaxiTest : BaseTest
    {
        private HomePage _homePage;
        private AirportTaxiPage _airportTaxiPage;
        private const string _expectedPickUpLocation = "Vilnius";
        private const string _expectedDestination = "Kaunas";
        private DateTime _expectedTaxiDate = new DateTime(2023, 12, 16);
        private const string _expectedHour = "13";
        private const string _expectedMinutes = "30";
        private const string _expectedTime = $"{_expectedHour}:{_expectedMinutes}";

        [SetUp]
        public void Setup()
        {
            _homePage = new HomePage(Driver);
            _airportTaxiPage = new AirportTaxiPage(Driver);
            _homePage.OpenAirportTaxiPage();
            Assert.That(Driver.Url.Contains("taxi"), Is.True, "Airport taxi page is not displayed");
        }

        [Test]
        public void BookAirportTaxi()
        {
            _airportTaxiPage.EnterPickUpLocation(_expectedPickUpLocation);
            _airportTaxiPage.EnterDestinationLocation(_expectedDestination);
            var actualPickUpLocation = _airportTaxiPage.GetPickUpLocation();
            var actualDropOffLocation = _airportTaxiPage.GetDropOffLocation();
            Assert.That(actualPickUpLocation.Contains(_expectedPickUpLocation), Is.True, "Pick-up is not selected");
            Assert.That(actualDropOffLocation.Contains(_expectedDestination), Is.True, "Drop-Off is not selected");

            _airportTaxiPage.ClickDateField();
            _airportTaxiPage.SelectDate(_expectedTaxiDate);
            var taxiDate = _airportTaxiPage.GetSelectedDate();
            var parsedDate = DateTime.ParseExact(taxiDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var expectedFormattedDate = _expectedTaxiDate.ToString("MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            var actualFormattedDate = parsedDate.ToString("MM/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            Assert.That(actualFormattedDate, Is.EqualTo($"{expectedFormattedDate}"), $"Selected airport taxi date is not {_expectedTaxiDate}");

            _airportTaxiPage.ClickTimeField();
            _airportTaxiPage.SelectHourValue(_expectedHour);
            _airportTaxiPage.SelectMinutesValue(_expectedMinutes);
            _airportTaxiPage.ConfirmTime();
            var taxiTime = _airportTaxiPage.GetPickUpTime();
            Assert.That(taxiTime, Is.EqualTo($"{_expectedTime}"), $" Selected taxi time is not {_expectedTime}");

            _airportTaxiPage.ClickSearch();
            _homePage.DeclineCookies();

            Assert.That(_airportTaxiPage.IsDisplayedList(), Is.True, "Taxis list is not displayed");
            _airportTaxiPage.SelectTaxi();
            _airportTaxiPage.ClickContinueButton();
          
            Assert.That(_airportTaxiPage.IsSummaryDisplayed(), Is.True, "Taxi travel itinerary is not displayed");
            Assert.That(Driver.Url.Contains("checkout"), Is.True, "Taxi travel itinerary is not displayed ");
        }
    }
}
