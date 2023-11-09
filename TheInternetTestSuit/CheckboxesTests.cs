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
            Assert.That(_checkboxesPage.CountCkeckboxes(), Is.EqualTo (2), "There are no two checkboxes displayed");
        }

        [Test]
        public void ToggleCheckboxes() 
        {
            Assert.That(_checkboxesPage.SelectFirstCheckbox(), Is.True, "'Checkbox 1' is not selected");
            Assert.That(_checkboxesPage.SelectFirstCheckbox(), Is.False, "'Checkbox 1' is selected");
        }

        [Test]
        public void CheckBothCheckboxes()
        {
            Assert.That(_checkboxesPage.SelectBothCheckboxes(), Is.True, "'Checkbox 1' and 'Checkbox 2' are not selected");
        }

        [Test]
        public void UncheckBothCheckboxes ()
        {
            CheckBothCheckboxes();
            Assert.That(_checkboxesPage.SelectBothCheckboxes(), Is.False, "'Checkbox 1' and 'Checkbox 2' are selected");
        }

        [Test]
        public void ToggleCheckboxesRepeatedly() 
        {
            ToggleCheckboxes();
            Assert.That(_checkboxesPage.SelectFirstCheckbox(), Is.True, "'Checkbox 1' is not selected");
            Assert.That(_checkboxesPage.SelectFirstCheckbox(), Is.False, "'Checkbox 1' is selected");
        }
    }
}
