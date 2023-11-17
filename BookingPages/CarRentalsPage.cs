using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BookingPages
{
    public class CarRentalsPage
    {

        private readonly IWebDriver _driver;
        private IWebElement SearchButton => _driver.WaitForElementVisible(By.CssSelector(".submit-button-container"));
        private IWebElement ErrorMessage => _driver.WaitForElementVisible(By.XPath("//*[text()='Please provide a pick-up location']"));
        private IWebElement PickUpLocation => _driver.WaitForElementClicable(By.CssSelector("#searchbox-toolbox-fts-pickup"));
        private IWebElement PickUpDateField => _driver.FindElement(By.XPath("//*[@id='searchbox-toolbox-date-picker-pickup-date']/div/div[2]/div/div[2]"));

        private IList<IWebElement> AllDates => PickUpDateField.FindElements(By.TagName("td"));

        private IWebElement ChosenPickUpDate => _driver.WaitForElementVisible(By.XPath("//*[@id=\"searchbox-toolbox-date-picker-pickup-date\"]/div/div[2]/div/div[2]"));

        public CarRentalsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void ClickSearchButton() => SearchButton.Click();

        public string GetErrorMessage() => ErrorMessage.Text;

        public void InputLocation(string invalidLocation) => PickUpLocation.SendKeys(invalidLocation);
        public string GetLocation() => PickUpLocation.GetAttribute("value");

        public void ClickPickUpDateField() => PickUpDateField.Click();

        public void SelectDate(DateTime dateToSelect)
        {
            ClickPickUpDateField();
            IList<IWebElement> currentMonthYearElements = _driver.FindElements(By.XPath("//*[@class='LPCM_3f5d2395']/h3"));
            var currentMonthYearText = currentMonthYearElements[0].Text;

            var desiredMonthYearText = dateToSelect.ToString("MMMM yyyy");
            while (currentMonthYearText != desiredMonthYearText)
            {

                IWebElement nextButton = _driver.FindElement(By.CssSelector(".LPCM_0cabfe7c"));
                nextButton.Click();

                currentMonthYearElements = _driver.FindElements(By.XPath("//*[@class='LPCM_3f5d2395']/h3"));
                currentMonthYearText = currentMonthYearElements[0].Text;

            }

            IWebElement desiredDayElement = _driver.FindElement(By.XPath($"//*[@id='b2runway_internal_actionPage']/div[3]/main/div[2]/div/div/div/div/div/div[2]/div/div[2]/div/div[1]/span/div/div[1]/div/div/div/div[1]/table/tbody/tr[3]/td[6]/span/span"));
            desiredDayElement.Click();
        }

        public string GetPickUpSelectedData() => ChosenPickUpDate.GetAttribute("value");

       // public void SelectDropOffDate() => DropOffDate.Click();

    }
}
