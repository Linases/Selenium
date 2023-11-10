using Welcome;
using Checkboxes;

namespace TheInternetTestSuit
{
    [TestFixture]
    public class CheckboxesTests: BaseTest
    {
        private WelcomePage _welcomePage;
        private CheckboxesPage _checkboxesPage;

        [SetUp]
        public void TestSetup()
        {
            Driver.Navigate().GoToUrl(MainUrl);
            _welcomePage = new WelcomePage(Driver);
            _checkboxesPage = new CheckboxesPage(Driver);
            _welcomePage.DisplayCkeckboxesPage();
            Assert.That(_checkboxesPage.GetCheckboxesCount(), Is.EqualTo (2), "There are no two checkboxes displayed");
        }

        [Test]
        public void UncheckBothCheckboxes ()
        {
            CheckBothCheckboxes();
            Assert.That(_checkboxesPage.AreBothCheckboxesUnchecked, Is.True, "'Checkbox 1' and 'Checkbox 2' are selected");
        }

        [Test]
        public void ToggleCheckboxRepeatedly() 
        {
            ToggleCheckbox();
            Assert.That(_checkboxesPage.IsFirstCheckboxChecked(), Is.True, "'Checkbox 1' is not selected");
            Assert.That(_checkboxesPage.IsFirstCheckboxChecked(), Is.False, "'Checkbox 1' is selected");
        }
     
        private void CheckBothCheckboxes()
        {
            Assert.That(_checkboxesPage.AreBothCheckboxesChecked(), Is.True, "'Checkbox 1' and 'Checkbox 2' are not selected");
        }

        private void ToggleCheckbox()
        {
            Assert.That(_checkboxesPage.IsFirstCheckboxChecked(), Is.True, "'Checkbox 1' is not selected");
            Assert.That(_checkboxesPage.IsFirstCheckboxChecked(), Is.False, "'Checkbox 1' is selected");
        }
    }
}
