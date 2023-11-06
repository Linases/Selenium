using Welcome;
using Checkboxes;

namespace TheInternetTestSuit
{
    [TestFixture]
    public class CheckboxesTests: BaseTest
    {
        private WelcomePage welcomePage;
        private CheckboxesPage checkboxesPage;

        [SetUp]
        public void TestSetup()
        {
            Driver.Navigate().GoToUrl(MainUrl);
            welcomePage = new WelcomePage(Driver);
            checkboxesPage = new CheckboxesPage(Driver);
            welcomePage.DisplayCkeckboxesPage();
            Assert.That(checkboxesPage.CountCkeckboxes(), Is.EqualTo (2), "There are no two checkboxes displayed");
        }

        [Test]
        public void ToggleCheckboxes() 
        {
            Assert.That(checkboxesPage.SelectFirstCheckbox(), Is.True, "'Checkbox 1' is not selected");
            Assert.That(checkboxesPage.SelectFirstCheckbox(), Is.False, "'Checkbox 1' is selected");
        }

        [Test]
        public void CheckBothCheckboxes()
        {
            Assert.That(checkboxesPage.SelectBothCheckboxes(), Is.True, "'Checkbox 1' and 'Checkbox 2' are not selected");
        }

        [Test]
        public void UncheckBothCheckboxes ()
        {
            CheckBothCheckboxes();
            Assert.That(checkboxesPage.SelectBothCheckboxes(), Is.False, "'Checkbox 1' and 'Checkbox 2' are selected");
        }

        [Test]
        public void ToggleCheckboxesRepeatedly() 
        {
            ToggleCheckboxes();
            Assert.That(checkboxesPage.SelectFirstCheckbox(), Is.True, "'Checkbox 1' is not selected");
           Assert.That(checkboxesPage.SelectFirstCheckbox(), Is.False, "'Checkbox 1' is selected");
        }
    }
}
