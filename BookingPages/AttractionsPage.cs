using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BookingPages
{
    public class AttractionsPage
    {
        private readonly IWebDriver _driver;

        private IWebElement SearchField => _driver.WaitForElementClicable(By.XPath("//input[@placeholder='Where are you going?']"));
        private IList<IWebElement> AutocompleteList => _driver.WaitForElementsVisible(By.CssSelector(".css-9dv5ti"));

        private IWebElement SelectDaysField => _driver.WaitForElementClicable(By.XPath("//*[text()='Select your dates']"));

        private IWebElement Calendar => _driver.WaitForElementClicable(By.CssSelector(".a10b0e2d13"));

        private IWebElement NextMonthArrow => Calendar.FindElement(By.XPath(".a10b0e2d13 button"));

        private IList<IWebElement> CurrentMonths => Calendar.FindElements(By.CssSelector(".a10b0e2d13 h3"));

        private IList<IWebElement> CurrentDays => Calendar.FindElements(By.CssSelector(".a10b0e2d13 td"));

        private IWebElement SearchButton => _driver.WaitForElementVisible(By.XPath("//*[@type = 'submit']"));

        private IWebElement AttractionsResultsNumber => _driver.WaitForElementVisible(By.CssSelector(".css-kyr0ak h2"));
        public AttractionsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EnterDestination(string destination) => SearchField.SendKeys(destination);

        public string GetDestination() => SearchField.GetAttribute("value");

        public void GetFirstRelevantValue(string destination)
        {
            AutocompleteList.FirstOrDefault(x => x.GetAttribute("city") == $"{destination}").Click();
        }

        public void ClickDatesField() => SelectDaysField.Click();

        public void SelectDate(DateTime dateToSelect)
        {
            var currentMonthYearText = CurrentMonths.Select(x => x.Text).ToList();

            var desiredMonthYearText = dateToSelect.ToString("MMMM yyyy");
            while (!currentMonthYearText.Contains(desiredMonthYearText))
            {
                NextMonthArrow.Click();
            }

            CurrentDays.FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}")).Click();
        }
        public void ClickSearchButton() => SearchButton.Click();

        public string GetNumbetOfAttractions() => AttractionsResultsNumber.Text;
    }
}
