using Booking_Pages;
using BookingPages;
using NUnit.Framework;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class CarRentalTests : BaseTest
    {
        private HomePage _homePage;
        private CarRentalsPage _carRentalsPage;
        private Random random = new Random();
        private static DateTime pickUpDate = new DateTime(2023, 11, 25);
        private static DateTime dropOffDate = new DateTime(2023, 11, 19);
        private static DateTime expectedPickUpDate = dropOffDate;

        [SetUp]
        public void Setup()
        {
            _homePage = new HomePage(Driver);
            _carRentalsPage = new CarRentalsPage(Driver);
            _homePage.OpenCarRentalsPage();
            Assert.That(Driver.Url.Contains("cars/"), Is.True, "Car rentals page is not displayed");
        }

        [Test]
        public void CarRentalsNegativeCheck()
        {
            _carRentalsPage.ClickSearchButton();
            Assert.That(_carRentalsPage.GetErrorMessage(), Is.EqualTo("Please provide a pick-up location"));
            string invalidLocation = RandomGenerate(10);

            _carRentalsPage.InputLocation(invalidLocation);
            var location = _carRentalsPage.GetLocation();
            Assert.That(location, Is.EqualTo(invalidLocation), $"Entered location is not {invalidLocation}");
            _carRentalsPage.ClickSearchButton();
            Assert.That(_carRentalsPage.GetErrorMessage, Is.EqualTo("Please provide a pick-up location"));

            _carRentalsPage.ClickPickUpDateField();
            _carRentalsPage.SelectDate(pickUpDate);
            _carRentalsPage.SelectDate(dropOffDate);
            var PickUpDate = _carRentalsPage.GetPickUpDate();
            DateTime parsedDate = DateTime.ParseExact(PickUpDate, "ddd, MMM dd", System.Globalization.CultureInfo.InvariantCulture);
            Assert.That(parsedDate, Is.EqualTo(expectedPickUpDate), "Pick up day is not eaqual Drop Of day");
        }

        private string RandomGenerate(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrsqtuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
