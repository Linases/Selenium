using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functionality_Tests_Suit.FactoryPattern
{
    public class BrowserFactory
    {
        private static IWebDriver Driver;
        
        public static IWebDriver GetDriver(BrowserType browserType)
        {
            if (Driver == null)
            {
                Driver = CreateDriverInstance(browserType);
            }
            return Driver;
        }

        private static IWebDriver CreateDriverInstance(BrowserType browserType)
        {
            IWebDriver Driver = null;
            switch (browserType)
            {
                case BrowserType.Firefox:
                    {
                        Driver = new FirefoxDriver();
                        break;
                    }
                case BrowserType.Chrome:
                    {
                        Driver = new ChromeDriver();
                        break;
                    }
                default:
                    throw new NotSupportedException($"Browser type '{browserType}' is not supported.");
            }
            return Driver;
        }

        public enum BrowserType
        {
            Firefox,
            Chrome
        }
         
        public static void CloseDriver()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver = null;
            }
        }
    }
}
