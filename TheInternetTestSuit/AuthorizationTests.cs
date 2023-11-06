global using NUnit.Framework;
using Authentication;
using Welcome;

namespace TheInternetTestSuit
{
    [TestFixture]
    public class Authorization : BaseTest
    {
        private LoginPage loginPage;
        private LogoutPage logoutPage;
        private WelcomePage welcomePage;
        private SecureAreaPage secureAreaPage;

        [SetUp]
        public void TestSetup()
        {
            Driver.Navigate().GoToUrl(MainUrl);
            loginPage = new LoginPage(Driver);
            welcomePage = new WelcomePage(Driver);
            secureAreaPage = new SecureAreaPage(Driver);
            logoutPage = new LogoutPage(Driver);
            welcomePage.DisplayLoginPage();
            Assert.That(Driver.Url, Is.EqualTo($"{MainUrl}/login"), "Login page is not displayed");
        }

        [Test]
        public void ValidLogin()
        {
            loginPage.Login("tomsmith", "SuperSecretPassword!");
            Assert.That(secureAreaPage.GetLoginMessage().Contains("You logged into a secure area!"));
        }

        [Test]
        public void InvalidLogin()
        {
            loginPage.InvalidLogin("invalid", "invalid");
            Assert.That(loginPage.GetErrorMessage().Contains("Your username is invalid!"));
        }

        [Test]
        public void LogoutAfterLogin()
        {
            ValidLogin();
            logoutPage.ClickLogoutButton();
            Assert.That(logoutPage.GetLogoutMessage().Contains("You logged out of the secure area!"));
        }

        [Test]
        public void LoginWithEmptyCredentials()
        {
            loginPage.InvalidLogin(string.Empty, string.Empty);
            Assert.That(loginPage.GetErrorMessage().Contains("Your username is invalid!"));
        }
    }
}
