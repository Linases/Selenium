global using NUnit.Framework;
using Authentication;
using Welcome;

namespace TheInternetTestSuit
{
    [TestFixture]
    public class Authorization : BaseTest
    {
        private LoginPage _loginPage;
        private LogoutPage _logoutPage;
        private WelcomePage _welcomePage;
        private SecureAreaPage _secureAreaPage;

        [SetUp]
        public void TestSetup()
        {
            Driver.Navigate().GoToUrl(MainUrl);
            _loginPage = new LoginPage(Driver);
            _welcomePage = new WelcomePage(Driver);
            _secureAreaPage = new SecureAreaPage(Driver);
            _logoutPage = new LogoutPage(Driver);
            _welcomePage.OpenLoginPage();
            Assert.That(Driver.Url, Is.EqualTo($"{MainUrl}/login"), "Login page is not displayed");
        }

        [Test]
        public void ValidLogin()
        {
            _loginPage.Login("tomsmith", "SuperSecretPassword!");
            Assert.That(_secureAreaPage.GetLoginMessage().Contains("You logged into a secure area!"));
        }

        [Test]
        public void InvalidLogin()
        {
            _loginPage.InvalidLogin("invalid", "invalid");
            Assert.That(_loginPage.GetErrorMessage().Contains("Your username is invalid!"));
        }

        [Test]
        public void LogoutAfterLogin()
        {
            ValidLogin();
            _logoutPage.ClickLogoutButton();
            Assert.That(_logoutPage.GetLogoutMessage().Contains("You logged out of the secure area!"));
        }

        [Test]
        public void LoginWithEmptyCredentials()
        {
            _loginPage.InvalidLogin(string.Empty, string.Empty);
            Assert.That(_loginPage.GetErrorMessage().Contains("Your username is invalid!"));
        }
    }
}
