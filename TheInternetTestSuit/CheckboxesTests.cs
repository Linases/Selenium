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
            _checkboxesPage.SelectFirstCheckbox();
            _checkboxesPage.SelectSecondCheckbox();
            Assert.That(_checkboxesPage.AreBothCheckboxesChecked, Is.False, "'Checkbox 1' and 'Checkbox 2' are selected");
        }

        [Test]
        public void ToggleCheckboxRepeatedly() 
        {
            ToggleCheckbox();
            _checkboxesPage.SelectFirstCheckbox();
            Assert.That(_checkboxesPage.IsFirstCheckboxChecked(), Is.True, "'Checkbox 1' is not selected");
            _checkboxesPage.SelectFirstCheckbox();
            Assert.That(_checkboxesPage.IsFirstCheckboxChecked(), Is.False, "'Checkbox 1' is selected");
        }
     
        private void CheckBothCheckboxes()
        {
            _checkboxesPage.SelectFirstCheckbox();
            Assert.That(_checkboxesPage.AreBothCheckboxesChecked(), Is.True, "'Checkbox 1' and 'Checkbox 2' are not selected");
        }

        private void ToggleCheckbox()
        {
            _checkboxesPage.SelectFirstCheckbox();
            Assert.That(_checkboxesPage.IsFirstCheckboxChecked(), Is.True, "'Checkbox 1' is not selected");
            _checkboxesPage.SelectFirstCheckbox();
            Assert.That(_checkboxesPage.IsFirstCheckboxChecked(), Is.False, "'Checkbox 1' is selected");
        }
    }
}
