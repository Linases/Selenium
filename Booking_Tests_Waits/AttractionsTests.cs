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
    public class AttractionsTests : BaseTest
    {
        private HomePage _homePage;
        private AttractionsPage _attractionsPage;
        private const string expectedDestination = "Vilnius";
        DateTime firstDay = new DateTime(2023, 11, 18);
        DateTime lastDay = new DateTime(2023, 11, 20);

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
            // _attractionsPage.GetFirstRelevantValue(expectedDestination);
            //var destination = _attractionsPage.GetDestination();
            // Assert.That(destination, Is.EqualTo(expectedDestination));

            _attractionsPage.ClickDatesField();
            _attractionsPage.SelectDate(firstDay);
            _attractionsPage.SelectDate(lastDay);
            _attractionsPage.ClickSearchButton();



        }



    }
}




