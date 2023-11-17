using BookingPages;
using NUnit.Framework;

namespace Booking_Tests_Waits
{
    [TestFixture]
    internal class LanguageTests : BaseTest
    {
        private LanguagePage _languagePage;
        private string existingLanguage = "English";
        private string changedLanguage = "Nederlands";

        [SetUp]
        public void Setup()
        {
            _languagePage = new LanguagePage(Driver);
        }

        [Test]
        public void LanguageChange()
        {
            var languageNow = _languagePage.GetButtonLanguageName(existingLanguage);
            Assert.That(languageNow, Is.EqualTo(existingLanguage), $"Language now is not {existingLanguage}");
            _languagePage.ClickLanguageButton();
            _languagePage.SelectLanguge();
            var languageNew = _languagePage.GetButtonLanguageName(changedLanguage);
            Assert.That(languageNew, Is.EqualTo(changedLanguage), $" Changed language is not {changedLanguage}");
        }
    }
}
