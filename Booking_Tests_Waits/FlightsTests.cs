using Booking_Pages;
using BookingPages;
using NUnit.Framework;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class FlightsTests : BaseTest
    {
        private HomePage _homePage;
        private FlightsPage _flightsPage;
        private List<string> expectedDepartureList = new List<string> { "Kaunas", "Vilnius", "Madrid" };
        private List<string> expectedDestinationList = new List<string> { "Paris", "Vilnius", "Kaunas" };
        private List<DateTime> expectedDates = new List<DateTime>
        {
           new DateTime(2023, 11, 18),
           new DateTime(2023, 11, 19),
           new DateTime(2023, 11, 20)
        };

        [SetUp]
        public void Setup()
        {
            _homePage = new HomePage(Driver);
            _flightsPage = new FlightsPage(Driver);
            _homePage.OpenFlightsPage();
            var flightsUrl = Driver.Url;
            Assert.That(flightsUrl.Contains("https://booking.kayak.com/"), "Flights page is not displayed");
        }

        [Test]
        public void MultiCityFlightSearch()
        {
            _flightsPage.PressReturn();
            _flightsPage.SelectMulticityMode();
            Assert.That(_flightsPage.CountMultipleSearchForms(), Is.EqualTo(3), "3 Search forms did not appear");

            SelectPlaces();

            SelectDates();

            _flightsPage.ClickAdButton();
            Assert.That(_flightsPage.CountMultipleSearchForms(), Is.EqualTo(4), "4 Search forms did not appear");
            expectedDepartureList.Add("Paris");
            expectedDestinationList.Add("Vilnius");
            _flightsPage.SendKeysToDepartures(_flightsPage.DepartureInputFieldsToList(), expectedDepartureList);
            var EnteredDepartures4leg = _flightsPage.GetEnteredDepartures();
            Assert.That(EnteredDepartures4leg, Is.EqualTo(expectedDepartureList), "Entered and expected departures are not equal");
            _flightsPage.SendKeysToDestinatons(_flightsPage.DestinationsInputFieldsToList(), expectedDestinationList);
            var EnteredDestinations4leg = _flightsPage.GetEnteredDestinations();
            Assert.That(EnteredDestinations4leg, Is.EqualTo(expectedDestinationList), "Entered and expected destinations are not equal");

            expectedDates.Add(new DateTime(2023, 11, 23));
            _flightsPage.SelectDates(_flightsPage.DatesFieldsToList(), expectedDates);
            var selectedDates4leg = _flightsPage.GetDates();
            var expectedDateStrings4Leg = expectedDates.Select(date => date.ToString("yyyy-MM-dd")).ToList();
            Assert.That(selectedDates4leg, Is.EqualTo(expectedDateStrings4Leg), "Entered and expected dates are not equal");

            _flightsPage.RemoveLastLeg();
            Assert.That(_flightsPage.CountMultipleSearchForms(), Is.EqualTo(3), "Recently added leg of the journey is displayed");
            _flightsPage.ClickSearch();
            var flightsSearchPage = Driver.Url; ;
            Assert.That(flightsSearchPage.Contains("flights"), Is.True, "Available flights are not displayed");
        }
       
        private void SelectPlaces()
        {
            _flightsPage.SendKeysToDepartures(_flightsPage.DepartureInputFieldsToList(), expectedDepartureList);
            var EnteredDepartures = _flightsPage.GetEnteredDepartures();
            Assert.That(EnteredDepartures.Contains($"{expectedDepartureList}"), Is.True, "Entered and expected departures are not equal");

            _flightsPage.SendKeysToDestinatons(_flightsPage.DestinationsInputFieldsToList(), expectedDestinationList);
            var EnteredDestinations = _flightsPage.GetEnteredDestinations();
            Assert.That(EnteredDestinations, Is.EqualTo(expectedDestinationList), "Entered and expected destinations are not equal");
        }

        private void SelectDates()
        {
            _flightsPage.SelectDates(_flightsPage.DatesFieldsToList(), expectedDates);
            var selectedDates = _flightsPage.GetDates();
            var expectedDateStrings = expectedDates.Select(date => date.ToString("yyyy-MM-dd")).ToList();
            Assert.That(selectedDates, Is.EqualTo(expectedDateStrings), "Entered and expected dates are not equal");
        }
    }
}

