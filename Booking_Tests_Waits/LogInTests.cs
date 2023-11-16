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
    public class LogInTests : BaseTest
    {
        private LoginPage _loginPage;
        private const string expectedEmail = "dsafkjf@gmail.com";

        [SetUp]
        public void Setup()
        {
            _loginPage = new LoginPage(Driver);
            _loginPage.ClickSignIn();
             Assert.That(Driver.Url.Contains($"/sign-in"), Is.True, "Login Page page is not displayed");
        }

        [Test]
        public void EnterInvalidLogin()
        {
            _loginPage.EnterEmail(expectedEmail);
            var email = _loginPage.GetEmail();
            Assert.That(email, Is.EqualTo(expectedEmail), $"Entered email is not: {expectedEmail} ");
            _loginPage.ClickContinueButton();
            Assert.That(_loginPage.IsPasswordFieldVisible(), Is.True, "Password field is not visible");
            Assert.That(_loginPage.IsConfirmPasswordFieldVisible(), Is.True, " Confirm password field is not visible");
        }
    }
}
