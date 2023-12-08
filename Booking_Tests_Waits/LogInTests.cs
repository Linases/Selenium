using BookingPages;
using NUnit.Framework;
using Utilities;

namespace Booking_Tests_Waits
{
    [TestFixture]
    public class LogInTests : BaseTest
    {
        private LoginPage _loginPage;
        private string _expectedEmail = $"{RandomHelper.RandomGenerate(8)}@gmail.com";

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
            _loginPage.EnterEmail(_expectedEmail);
            var email = _loginPage.GetEmail();
            Assert.That(email, Is.EqualTo(_expectedEmail), $"Entered email is not: {_expectedEmail}");
            _loginPage.ClickContinueButton();
            var passwordField = _loginPage.IsPasswordFieldVisible();
            Assert.That(passwordField, Is.True, "Password field is not visible");
            var confirmPasswordField = _loginPage.IsConfirmPasswordFieldVisible();
            Assert.That(confirmPasswordField, Is.True, " Confirm password field is not visible");
        }
    }
}
