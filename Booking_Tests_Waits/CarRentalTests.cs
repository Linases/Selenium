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
        private Random _random = new Random();
        private static DateTime _pickUpDate = new DateTime(2023, 11, 30);
        private static DateTime _dropOffDate = new DateTime(2023, 11, 29);
       
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
            var message = "Please provide a pick-up location";
            Assert.That(_carRentalsPage.GetErrorMessage, Is.EqualTo(message), $"Error message is not: {message}");

            _carRentalsPage.ClickPickUpDateField();
            _carRentalsPage.SelectDate(_pickUpDate);
            _carRentalsPage.SelectDate(_dropOffDate);
            var pickUpDate = _carRentalsPage.GetPickUpDate();
            var dropOfDate = _carRentalsPage.GetDropOffDate();
            DateTime parsedPickUpDate = DateTime.ParseExact(pickUpDate, "ddd, MMM dd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime parsedDropOffDate = DateTime.ParseExact(dropOfDate, "ddd, MMM dd", System.Globalization.CultureInfo.InvariantCulture);
            Assert.That(parsedPickUpDate, Is.EqualTo(parsedDropOffDate.AddDays(-1)), "Pick up day is not eaqual Drop Of day-1");
        }

        private string RandomGenerate(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrsqtuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
