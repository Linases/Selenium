using Apache.NMS;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class SecureAreaPage
    {
        private readonly IWebDriver _driver;
        private readonly By validLoginMessage = By.Id("flash-messages");
       
        public SecureAreaPage (IWebDriver driver)
        {
            _driver = driver;
        }   

        public string GetLoginMessage()
        {
            return _driver.FindElement(validLoginMessage).Text;
        }
    }
}
