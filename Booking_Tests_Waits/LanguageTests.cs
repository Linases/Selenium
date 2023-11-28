using BookingPages;
using NUnit.Framework;

namespace Booking_Tests_Waits
{
    [TestFixture]
    internal class LanguageTests : BaseTest
    {
        private LanguagePage _languagePage;
        private string _existingLanguage = "English";
        private string _changedLanguage = "Italiano";

        [SetUp]
        public void Setup()
        {
            _languagePage = new LanguagePage(Driver);
        }

        [Test]
        public void LanguageChange()
        {
            var languageNow = _languagePage.GetButtonLanguageName();
            Assert.That(languageNow.Contains($"{_existingLanguage}"), $"Language now is not {_existingLanguage}");
            _languagePage.ClickLanguageButton();
            _languagePage.SelectLanguge(_changedLanguage);
            var languageNew = _languagePage.GetButtonLanguageName();
            Assert.That(languageNew.Contains($"{_changedLanguage}"), $" Changed language is not {_changedLanguage}");
        }
    }
}
