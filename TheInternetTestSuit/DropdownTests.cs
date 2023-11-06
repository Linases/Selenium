using Dropdown;
using Welcome;

namespace TheInternetTestSuit
{
    [TestFixture]
    public class DropdownTests : BaseTest
    {
        private WelcomePage welcomePage;
        private DropdownPage dropdownPage;

        [SetUp]
        public void TestSetup()
        {
            Driver.Navigate().GoToUrl(MainUrl);
            welcomePage = new WelcomePage(Driver);
            dropdownPage = new DropdownPage(Driver);
            welcomePage.DisplayDropdownPage();
            Assert.That(Driver.Url, Is.EqualTo($"{MainUrl}/dropdown"), "Dropdown page is not displayed");
        }

        [Test]
        public void SelectOption()
        {
            Assert.That(dropdownPage.SelectOption("Option 1"), Is.EqualTo("Option 1"), "Selected option is not 'Option 1'");
        }

        [Test]
        public void SelectDifferentOption()
        {
            SelectOption();
            Assert.That(dropdownPage.SelectOption("Option 2"), Is.EqualTo("Option 2"), "Selected option is not 'Option 2'");
        }

        [Test]
        public void ValidateDropdownOptions()
        {
            List<string> expectedOptions = new List<string> {"Option 1", "Option 2"};
            Assert.That(dropdownPage.ShowAllOptions(), Is.EqualTo(expectedOptions));
        }

        [Test]
        public void SelectRandomOption()
        {
            dropdownPage.SelectRandomOption();
            var selectedOption = dropdownPage.GetSelectRandomOption();
            Assert.That(selectedOption.Displayed, "The selected option is not displayed in the dropdown.");
        }
    }
}
