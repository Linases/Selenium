using Booking_Pages;
using BookingPages;
using NUnit.Framework;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class AirportTaxiTest : BaseTest
    {
        private HomePage _homePage;
        private AirportTaxiPage _airportTaxiPage;
        private const string expectedPickUpLocation = "Vilnius";
        private const string expectedDestination = "Paris";
        DateTime expectedTaxiDate = new DateTime(2023, 11, 18);
        private const string expectedHour = "13";
        private const string expectedMinutes = "30";
        private const string expectedTime = $"{expectedHour}:{expectedMinutes}";

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
            ChoosePickUpPlaceAndDestination();
            SelectTaxiDate();
            SelectTaxiTime();
            _airportTaxiPage.ClickSearch();
            SelectTaxi();
        }

        private void ChoosePickUpPlaceAndDestination()
        {
            _airportTaxiPage.EnterPickUpLocation(expectedPickUpLocation);
            _airportTaxiPage.EnterDestinationLocation(expectedDestination);
        }

        private void SelectTaxiDate()
        {
            _airportTaxiPage.ClickDateField();
            _airportTaxiPage.SelectDate(expectedTaxiDate);
            var taxiDate = _airportTaxiPage.GetSelectedDate();
            Assert.That(taxiDate, Is.EqualTo($"{expectedTaxiDate}"), $"Selected airport taxi date is not {expectedTaxiDate}");
        }

        private void SelectTaxiTime()
        {
            _airportTaxiPage.ClickTimeField();
            _airportTaxiPage.SelectHourValue(expectedHour);
            _airportTaxiPage.SelectMinutesValue(expectedMinutes);
            _airportTaxiPage.ConfirmTime();
            var taxiTime = _airportTaxiPage.GetPickUpTime();
            Assert.That(taxiTime, Is.EqualTo($"{expectedTime}"), $" Selected taxi time is not {expectedTime}");
        }

        private void SelectTaxi()
        {
            Assert.That(_airportTaxiPage.isDisplayedList(), Is.True, "Taxis list is not displayed");
            _airportTaxiPage.SelectTaxi();
            _airportTaxiPage.ClickContinueButton();
            Assert.That(_airportTaxiPage.SummaryDisplayed(), Is.True, "Taxi travel itinerary is not dispalyed");
            Assert.That(Driver.Url.Contains("checkout"), Is.True, "Taxi travel itinerary is not dispalyed ");
        }
    }
}
