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
            _welcomePage.OpenDropdownPage();
            Assert.That(Driver.Url, Is.EqualTo($"{MainUrl}/dropdown"), "Dropdown page is not displayed");
        }

        [Test]
        public void SelectDifferentOption()
        {
            SelectOption();
            _dropdownPage.ChooseOption("Option 2");
            Assert.That(_dropdownPage.GetSelectedOption, Is.EqualTo("Option 2"), "Selected option is not 'Option 2'");
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
            _dropdownPage.SelectRandomOption();
            Assert.That(_dropdownPage.IsRandomSelectedOptionDisplayed(), Is.True, "The selected option is not displayed in the dropdown.");
        }

        private void SelectOption()
        {
            _dropdownPage.ChooseOption("Option 1");
            Assert.That(_dropdownPage.GetSelectedOption, Is.EqualTo("Option 1"), "Selected option is not 'Option 1'");
        }
    }
}
