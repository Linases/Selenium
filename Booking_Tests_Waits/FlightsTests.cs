using Booking_Pages;
using BookingPages;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Globalization;
using Utilities;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class FlightsTests : BaseTest
    {
        private HomePage _homePage;
        private FlightsPage _flightsPage;
        private const string _expectedDepartureOne = "Kaunas";
        private const string _expectedDepartureTwo = "Malta";
        private const string _expectedDepartureThree = "Madrid";
        private const string _expectedDepartureFour = "Lisbon";
        private const string _expectedDestinationOne = "Paris";
        private const string _expectedDestinationTwo = "London";
        private const string _expectedDestinationThree = "Dallas";
        private const string _expectedDestinationFour = "Rome";

        private DateTime _expectedDateOne = new DateTime(2023, 12, 28);
        private DateTime _expectedDateTwo = new DateTime(2024, 01, 04);
        private DateTime _expectedDateThree = new DateTime(2024, 01, 11);
        private DateTime _expectedDateFour = new DateTime(2024, 01, 18);

        [SetUp]
        public void Setup()
        {
            _homePage = new HomePage(Driver);
            _flightsPage = new FlightsPage(Driver);
            _homePage.DissmissAlert();
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

            _flightsPage.EnterDepartureOneText(_expectedDepartureOne);
            var actualDepartureOne = _flightsPage.GetDepartureOne();
            Assert.That(actualDepartureOne.Contains(_expectedDepartureOne), Is.True, "Actual and expected first departure is not the same");
            _flightsPage.EnterDepartureTwoText(_expectedDepartureTwo);
            var actualDepartureTwo = _flightsPage.GetDepartureTwo();
            Assert.That(actualDepartureTwo.Contains(_expectedDepartureTwo), Is.True, "Actual and expected second departure is not the same");
            _flightsPage.EnterDepartureThreeText(_expectedDepartureThree);
            var actualDepartureThree = _flightsPage.GetDepartureThree();
            Assert.That(actualDepartureThree.Contains(_expectedDepartureThree), Is.True, "Actual and expected third departure is not the same");

            _flightsPage.EnterDestinationOneText(_expectedDestinationOne);
            var actualDestinationOne = _flightsPage.GetDestinationOne();
            Assert.That(actualDepartureOne.Contains(_expectedDepartureOne), Is.True, "Actual and expected first destination is not the same");
            _flightsPage.EnterDestinationTwoText(_expectedDestinationTwo);
            var actualDestinationTwo = _flightsPage.GetDestinationTwo();
            Assert.That(actualDestinationTwo.Contains(_expectedDestinationTwo), Is.True, "Actual and expected second destination is not the same");
            _flightsPage.EnterDestinationThreeText(_expectedDestinationThree);
            var actualDestinationThree = _flightsPage.GetDestinationThree();
            Assert.That(actualDestinationThree.Contains(_expectedDestinationThree), Is.True, "Actual and expected third destination is not the same");

            _flightsPage.SelectDepartureOneDate(_expectedDateOne);
            var actualDateOne = _flightsPage.GetDateOne();
            var expecteddDateOne = _expectedDateOne.ToString("ddd MM/dd", CultureInfo.InvariantCulture);
            Assert.That(actualDateOne, Is.EqualTo(expecteddDateOne), "Entered and actual first dates are not equal");
            _flightsPage.SelectDepartureTwoDate(_expectedDateTwo);
            var actualDateTwo = _flightsPage.GetDateTwo();
            var expectedDateTwo = _expectedDateTwo.ToString("ddd M/d", CultureInfo.InvariantCulture);
            Assert.That(actualDateTwo, Is.EqualTo(expectedDateTwo), "Entered and actual second dates are not equal");
            _flightsPage.SelectDepartureThreeDate(_expectedDateThree);
            var actualDateThree = _flightsPage.GetDateThree();
            var expecteddDateThree = _expectedDateThree.ToString("ddd M/d", CultureInfo.InvariantCulture);
            Assert.That(actualDateThree, Is.EqualTo(expecteddDateThree), "Entered and actual third dates are not equal");

            _flightsPage.ClickAdButton();
            Assert.That(_flightsPage.CountMultipleSearchForms(), Is.EqualTo(4), "4 Search forms did not appear");

            _flightsPage.EnterDepartureFourText(_expectedDepartureFour);
            var actualDepartureFour = _flightsPage.GetDepartureFour();
            Assert.That(actualDepartureFour.Contains(_expectedDepartureFour), Is.True, "Actual and expected fourth departure is not the same");
            _flightsPage.EnterDestinationFourText(_expectedDestinationFour);
            var actualDestinationFour = _flightsPage.GetDestinationFour();
            Assert.That(actualDepartureFour.Contains(_expectedDepartureFour), Is.True, "Actual and expected fourth destination is not the same");
            _flightsPage.SelectDepartureFourDate(_expectedDateFour);
            var actualDateFour = _flightsPage.GetDateFour();
            var expecteddDateFour = _expectedDateFour.ToString("ddd M/dd", CultureInfo.InvariantCulture);
            Assert.That(expecteddDateFour, Is.EqualTo(expecteddDateFour), "Entered and actual fourth dates are not equal");

            _flightsPage.RemoveLastLeg();
            Assert.That(_flightsPage.CountMultipleSearchForms(), Is.EqualTo(3), "Added leg of the journey is not displayed");
            _flightsPage.ClickSearch();
            Driver.GetWait().Until(ExpectedConditions.UrlContains("flights"));
            Assert.That(Driver.Url.Contains("flights"), Is.True, "Available flights are not displayed");
        }
    }
}
