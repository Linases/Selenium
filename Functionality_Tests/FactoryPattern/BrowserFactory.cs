using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using Functionality_Tests_Suit.Constants;

namespace Functionality_Tests_Suit.FactoryPattern
{
    public class BrowserFactory
    {
        private static IWebDriver _driver;

        public static IWebDriver GetDriver(BrowserType browserType)
        {
            if (_driver == null)
            {
                _driver = CreateDriverInstance(browserType);
            }
            return _driver;
        }

        private static IWebDriver CreateDriverInstance(BrowserType browserType)
        {
            IWebDriver _driver = null;
            switch (browserType)
            {
                case BrowserType.Firefox:
                    {
                        _driver = new FirefoxDriver();
                        break;
                    }
                case BrowserType.Chrome:
                    {
                        _driver = new ChromeDriver();
                        break;
                    }
                default:
                    throw new NotSupportedException($"Browser type '{browserType}' is not supported.");
            }
            return _driver;
        }

        public static void CloseDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
}
