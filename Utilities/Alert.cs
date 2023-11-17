using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Alert
    {

        private static readonly IWebDriver _driver;
        private static IWebElement DissmissGeniusAlert => _driver.WaitForElementClicable(By.CssSelector(".c0528ecc22 button"));
        private static IWebElement AlertBox => _driver.FindElement(By.CssSelector(".c0528ecc22"));
        public static void DissmissAlert ()
        {
            if (AlertBox.Displayed)
            {
                DissmissGeniusAlert.Click();
            }
        }
    }
}
