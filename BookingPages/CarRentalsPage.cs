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

        private IWebElement PickUpDate => _driver.WaitForElementVisible(By.XPath("//*[@data-testid ='searchbox-toolbox-date-picker-pickup-date-value']"));
        private IWebElement DropOffDate => _driver.WaitForElementVisible(By.XPath("//*[@data-testid ='searchbox-toolbox-date-picker-dropoff-date-value']"));
        private IWebElement Calendar => _driver.WaitForElementVisible(By.XPath("//*[@data-testid='bui-calendar']"));

        private IWebElement NextMonthArrow => Calendar.FindElement(By.XPath("//*[@data-testid='bui-calendar']/ button"));

        private IList<IWebElement> CurrentMonths => Calendar.FindElements(By.XPath("//*[@data-testid='bui-calendar']//h3"));

        private IList<IWebElement> CurrentDays => Calendar.FindElements(By.XPath("//*[@data-testid='bui-calendar']//td"));
       
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

     
        public string GetPickUpDate() => PickUpDate.Text;

        public string GetDropOffDate() => DropOffDate.Text;

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
    
    public string GetPickUpSelectedData() => ChosenPickUpDate.GetAttribute("value");
    }
}
