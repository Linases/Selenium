using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome;
using Checkboxes;
using OpenQA.Selenium;

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
            welcomePage = new WelcomePage(Driver);
            checkboxesPage = new CheckboxesPage(Driver);    
        }

        [Test]
        public void Checkboxes() 
        {
            DisplayCheckboxesPage();
            Assert.That(checkboxesPage.SelectFirstCheckbox(), Is.True, "'Checkbox 1' is not selected");
        }

        private void DisplayCheckboxesPage() 
        {
            welcomePage.DisplayCkeckboxesPage();
            Assert.That(checkboxesPage.CountCkeckboxes(), Is.EqualTo (2), "There are no two checkboxes displayed");
        }
    }
}
