using Apache.NMS;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkboxes
{
    public class CheckboxesPage
    {
        private readonly IWebDriver _driver;
        private readonly By countCheckboxes = By.XPath("//*[@type='checkbox']");
        private readonly By firstCheckbox = By.XPath("//*[@id='checkboxes']/input[1]");
        
        public CheckboxesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public int CountCkeckboxes()
        {
           return _driver.FindElements(countCheckboxes).Count();
        }

        public bool SelectFirstCheckbox()
        {
            _driver.FindElement(firstCheckbox).Click();
            bool isChecked = _driver.FindElement(firstCheckbox).Selected;
            return isChecked;
        }

    }
}

