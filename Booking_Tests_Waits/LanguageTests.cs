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
    internal class LanguageTests : BaseTest
    {
        private LanguagePage _languagePage;
        private string language = "English";
        private string changedLanguage = "Nederlands";

        [SetUp]
        public void Setup()
        {
            _languagePage = new LanguagePage(Driver);
        }

        [Test]
        public void LanguageChange()
        {
           
            Assert. That(_languagePage.GetButtonLanguageName(language), Is.EqualTo(language));
            Thread.Sleep(2000);
            _languagePage.ClickLanguageButton();
            _languagePage.SelectLanguge();
            Assert.That(_languagePage.GetButtonLanguageName(changedLanguage), Is.EqualTo(changedLanguage));
      
        }
    }
}
