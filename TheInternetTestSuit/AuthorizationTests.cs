global using NUnit.Framework;
using Functionality_Tests_Suit.Constants;
using Functionality_Tests_Suit.FactoryPattern;
using Authentication;
using Welcome;
using OpenQA.Selenium;
using System.Diagnostics.Metrics;

namespace TheInternetTestSuit
{
    [TestFixture]
    public class Authorization: BaseTest
    {
        private LoginPage loginPage;
        private LogoutPage logoutPage;
        private WelcomePage welcomePage;
        private SecureAreaPage secureAreaPage;

        [SetUp]
        public void TestSetup()
        {
            loginPage = new LoginPage(Driver);
            welcomePage= new WelcomePage(Driver);
            secureAreaPage = new SecureAreaPage(Driver);
            logoutPage = new LogoutPage(Driver);
        }

        [Test]
        public void ValidLogin()
        {
            DisplayLoginPage();
            loginPage.ValidLogin("tomsmith", "SuperSecretPassword!");
            Assert.That(secureAreaPage.GetLoginMessage().Contains("You logged into a secure area!"));
        }

        [Test]
        public void InvalidLogin()
        {
            DisplayLoginPage();
            loginPage.InvalidLogin("invalid", "invalid");
            Assert.That(loginPage.GetErrorMessage().Contains("Your username is invalid!"));
        }

        [Test]
        public void LogoutAfterLogin()
        {
            ValidLogin();
            logoutPage.ClickLogoutButton();
            Assert.That(logoutPage.getLogoutMessage().Contains("You logged out of the secure area!"));
        }

        [Test]
        public void LoginWithEmptyCredentials ()
        {
            DisplayLoginPage();
            loginPage.InvalidLogin(string.Empty, string.Empty);
            Assert.That(loginPage.GetErrorMessage().Contains("Your username is invalid!"));
        }

        private void DisplayLoginPage()
        {
            welcomePage.DisplayLoginPage();
            Assert.That(Driver.Url, Is.EqualTo($"{MainUrl}/login"), "Login page is not displayed");
        }

       
    }
}
