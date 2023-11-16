using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BookingPages
{
    public class CarRentalsDatePickerPage
    {
        private readonly IWebDriver _driver;
        private IWebElement PickUpDate => _driver.WaitForElementVisible(By.XPath("//*[@data-testid ='searchbox-toolbox-date-picker-pickup-date-value']"), 20);
        private IWebElement DropOffDate=> _driver.WaitForElementVisible(By.XPath("//*[@data-testid ='searchbox-toolbox-date-picker-dropoff-date-value']"),20);
        private IWebElement Calendar => _driver.NoSuchElementExceptionWait(By.XPath("//*[@data-testid='bui-calendar']"), 20, 500);

        private IWebElement NextMonthArrow => Calendar.FindElement(By.XPath("//*[@data-testid='bui-calendar']/ button"));

        private IList<IWebElement> CurrentMonths => Calendar.FindElements(By.XPath("//*[@data-testid='bui-calendar']//h3"));

        private IList<IWebElement> CurrentDays => Calendar.FindElements(By.XPath("//*[@data-testid='bui-calendar']//td"));
      
          
        public CarRentalsDatePickerPage(IWebDriver driver)
        {
            _driver = driver;
        }
     
        public void ClickPickUpDateField() => PickUpDate.Click();
        public string GetPickUpDate ()  => PickUpDate.Text;
        public string GetDropOffDate ()  => DropOffDate.Text;

        public void SelectDate(DateTime dateToSelect)
        {
            var currentMonthYearText = CurrentMonths.Select(x => x.Text).ToList();

            var desiredMonthYearText = dateToSelect.ToString("MMMM yyyy");
            while (!currentMonthYearText.Contains(desiredMonthYearText))
            {
                NextMonthArrow.Click();
              //Thread.Sleep(2000);
            }


           CurrentDays.FirstOrDefault(element => element.Text.Contains($"{dateToSelect.Day}")).Click();
            
        }

        
        


    }
}
