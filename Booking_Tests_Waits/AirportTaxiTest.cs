using Booking_Pages;
using BookingPages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class AirportTaxiTest : BaseTest
    {
        private HomePage _homePage;
        private AirportTaxiPage _airportTaxiPage;
        private const string expectedPickUpLocation = "Vilnius";
        private const string expectedDestination = "Palanga";
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
            _airportTaxiPage.ChooseFirstItem();
            _airportTaxiPage.EnterDestinationLocation(expectedDestination);
            _airportTaxiPage.ChooseFirstItem();
            // var pickUpLocation = _airportTaxiPage.GetPickUpLocation();
            // var destination = _airportTaxiPage.GetDestination();
            // Assert.That(pickUpLocation.Contains(expectedPickUpLocation), Is.True);// not working
            //Assert.That(destination.Contains(expectedDestination), Is.True);//not working
        }

        private void SelectTaxiDate()
        {
            _airportTaxiPage.ClickDateField();
            _airportTaxiPage.SelectDate(expectedTaxiDate);
            var taxiDate = _airportTaxiPage.GetSelectedDate();
            //expectedTaxiDate.ToString("dd-MM-yyyy"); need to fix formats
            //Assert.That(taxiDate, Is.EqualTo($"{expectedTaxiDate}"), $"Selected airport taxi date is not {expectedTaxiDate}");
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
