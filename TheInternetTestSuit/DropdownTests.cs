using Dropdown;
using Welcome;

namespace TheInternetTestSuit
{
    [TestFixture]
    public class DropdownTests : BaseTest
    {
        private WelcomePage _welcomePage;
        private DropdownPage _dropdownPage;

        [SetUp]
        public void TestSetup()
        {
            Driver.Navigate().GoToUrl(MainUrl);
            _welcomePage = new WelcomePage(Driver);
            _dropdownPage = new DropdownPage(Driver);
            _welcomePage.DisplayDropdownPage();
            Assert.That(Driver.Url, Is.EqualTo($"{MainUrl}/dropdown"), "Dropdown page is not displayed");
        }

        [Test]
        public void SelectDifferentOption()
        {
            SelectOption();
            Assert.That(_dropdownPage.SelectOption("Option 2"), Is.EqualTo("Option 2"), "Selected option is not 'Option 2'");
        }

        [Test]
        public void ValidateDropdownOptions()
        {
            var expectedOptions = new List<string> { "Option 1", "Option 2" };
            Assert.That(_dropdownPage.GetAllOptions(), Is.EqualTo(expectedOptions));
        }

        [Test]
        public void SelectRandomOption()
        {
            Assert.That(_dropdownPage.IsSelectedOptionDisplayed(), Is.True, "The selected option is not displayed in the dropdown.");
        }

        private void SelectOption()
        {
            Assert.That(_dropdownPage.SelectOption("Option 1"), Is.EqualTo("Option 1"), "Selected option is not 'Option 1'");
        }
    }
}
