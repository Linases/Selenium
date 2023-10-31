using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class LogoutPage
    {
        private readonly IWebDriver _driver;
        private readonly By logoutButton = By.CssSelector("[href*='logout']");
        private readonly By logoutMessage = By.Id("flash-messages");

        public LogoutPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void ClickLogoutButton() => _driver.FindElement(logoutButton).Click();

        public string getLogoutMessage ()
        {
            return _driver.FindElement(logoutMessage).Text;
        }
    }
}
    

