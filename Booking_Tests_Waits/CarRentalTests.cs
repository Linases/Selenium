using Booking_Pages;
using BookingPages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class CarRentalTests : BaseTest
    {
        private HomePage _homePage;
        private CarRentalsPage _carRentalsPage;
        private CarRentalsDatePickerPage _carRentalsDatePickerPage;

        private const string invalidLocation = "hjfkhf";
        DateTime pickUpDate = new DateTime(2023, 11, 18);
        DateTime dropOffDate = new DateTime(2023, 11, 20);

        [SetUp]
        public void Setup()
        {
            _homePage = new HomePage(Driver);
            _carRentalsPage = new CarRentalsPage(Driver);
            _carRentalsDatePickerPage = new CarRentalsDatePickerPage(Driver);
            _homePage.OpenCarRentalsPage();
            Assert.That(Driver.Url.Contains($"{MainUrl}/cars/"), Is.True, "Car rentals page is not displayed");
        }

        [Test]
        public void CarRentalsPageOpened()
        {
            _carRentalsPage.ClickSearchButton();
            Assert.That(_carRentalsPage.GetErrorMessage(), Is.EqualTo("Please provide a pick-up location"));
            _carRentalsPage.InputLocation(invalidLocation);
            var location = _carRentalsPage.GetLocation();
            Assert.That(location, Is.EqualTo(invalidLocation), $"Entered location is not {invalidLocation}");
            _carRentalsPage.ClickSearchButton();
            Assert.That(_carRentalsPage.GetErrorMessage, Is.EqualTo("Please provide a pick-up location"));
            _carRentalsDatePickerPage.ClickPickUpDateField();
           
            _carRentalsDatePickerPage.SelectDate(pickUpDate);
            _carRentalsDatePickerPage.SelectDate(dropOffDate);


        }
    }
}
