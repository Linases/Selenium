using Booking_Pages;
using BookingPages;
using NUnit.Framework;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class AttractionsTests : BaseTest
    {
        private HomePage _homePage;
        private AttractionsPage _attractionsPage;
        private const string expectedDestination = "Paris";
        private DateTime attractionsDay = new DateTime(2023, 11, 26);

        [SetUp]
        public void Setup()
        {
            _homePage = new HomePage(Driver);
            _attractionsPage = new AttractionsPage(Driver);
            _homePage.OpenAttractionsPage();
            Assert.That(Driver.Url.Contains($"attractions"), Is.True, "Attractions page is not displayed");
        }

        [Test]
        public void SearchingAttractions()
        {
            _attractionsPage.EnterDestination(expectedDestination);
            _attractionsPage.SelectAutocompleteOption();
            var destination = _attractionsPage.GetDestination();

            Assert.That(destination, Is.EqualTo(expectedDestination));

            _attractionsPage.ClickDatesField();
            _attractionsPage.SelectDate(attractionsDay);
            var actualDay = _attractionsPage.GetAttractionsDate();
            var expectedDay = attractionsDay.ToString("MMM dd");

            Assert.That(actualDay, Is.EqualTo(expectedDay));
            _attractionsPage.ClickSearchButton();

            Assert.That(Driver.Url.Contains($"attractions/searchresults"), Is.True, "Attractions searchresults are not displayed");
            _attractionsPage.SelecFirstAvailability();
            Assert.That(_attractionsPage.IsAttractionDetailsDisplayed(), Is.True, "The details page for the selected attraction is not displayed");
            Assert.That(_attractionsPage.IsDatePickerDisplayed(), Is.True, "Available dates for the selected attraction are not displayed");
            Assert.That(_attractionsPage.IsTimeSlotDisplayed(), Is.True, "Available times for the selected attraction are not displayed");
        }
    }
}
